#if UNITY_IOS
namespace Shopify.Unity.SDK.iOS {
    using System.Collections.Generic;
    using System.Collections;
    using System;
    using Shopify.Unity.MiniJSON;
    using Shopify.Unity.SDK;

    public partial class iOSNativeCheckout : IApplePayEventReceiver {
        /// These are the Shipping Address Fields we send as part of a MailingAddressInput
        private static readonly string[] ShippingAddressFields = {
            MailingAddressInput.address1FieldKey,
            MailingAddressInput.address2FieldKey,
            MailingAddressInput.cityFieldKey,
            MailingAddressInput.countryFieldKey,
            MailingAddressInput.provinceFieldKey,
            MailingAddressInput.zipFieldKey
        };

        private static readonly HashSet<string> ShippingAddressFieldsSet = new HashSet<string>(ShippingAddressFields);

        /// These are the Contact Fields we send as part of a MailingAddressInput
        private static readonly string[] ShippingContactFields = {
            MailingAddressInput.firstNameFieldKey,
            MailingAddressInput.lastNameFieldKey,
            MailingAddressInput.phoneFieldKey
        };

        private static readonly HashSet<string> ShippingContactFieldsSet = new HashSet<string>(ShippingContactFields);

        /// These are the Shipping Address Fields we send as part of a MailingAddressInput when
        /// we only have the partial shipping address before payment authentication
        private static readonly string[] PartialShippingAddressFields = {
            MailingAddressInput.cityFieldKey,
            MailingAddressInput.countryFieldKey,
            MailingAddressInput.provinceFieldKey,
            MailingAddressInput.zipFieldKey
        };

        private static readonly HashSet<string> PartialShippingAddressFieldsSet = new HashSet<string>(PartialShippingAddressFields);

        private delegate void ApplePayEventHandlerCompletion(ApplePayEventResponse errorResponse);

        private enum NativePaymentStatus {
            Success,
            Cancelled,
            Failed
        }

        private struct UpdateRequestStatus {
            public readonly ApplePayAuthorizationStatus AuthorizationStatus;
            public readonly List<ApplePayError> Errors;

            public UpdateRequestStatus(ApplePayAuthorizationStatus status, List<ApplePayError> errors = null) {
                AuthorizationStatus = status;
                Errors = errors;
            }
        }

        public void UpdateSummaryItemsForShippingIdentifier(string serializedMessage) {
            var message = NativeMessage.CreateFromJSON(serializedMessage);

            CartState.SetShippingLine(message.Content, (ShopifyError error) => {
                if (error == null) {
                    var summaryItems = GetSummaryItemsFromCheckout(CartState.CurrentCheckout);
                    message.Respond(new ApplePayEventResponse(ApplePayAuthorizationStatus.Success, summaryItems).ToJsonString());
                } else {
                    message.Respond(new ApplePayEventResponse(ApplePayAuthorizationStatus.Failure).ToJsonString());
                }
            });
        }

        public void UpdateSummaryItemsForShippingContact(string serializedMessage) {
            var message = NativeMessage.CreateFromJSON(serializedMessage);
            var contentDictionary = (Dictionary<string, object>) Json.Deserialize(message.Content);
            var mailingAddressInput = new MailingAddressInput(contentDictionary);

            CartState.SetShippingAddress(mailingAddressInput, (ShopifyError error) => {
                if (error == null) {
                    RespondToUpdateAddressSuccessForMessage(message);
                } else {
                    RespondToUpdateAddressErrorForMessage(message, error);
                }
            });
        }

        private static bool IsShippingAddressField(string field) {
            return ShippingAddressFieldsSet.Contains(field);
        }

        private static bool IsContactField(string field) {
            return ShippingContactFieldsSet.Contains(field);
        }

        private static bool IsPartialShippingAddressField(string field) {
            return PartialShippingAddressFieldsSet.Contains(field);
        }

        private static bool IsEmailField(string field) {
            return field == "email";
        }

        private static UpdateRequestStatus GetUpdateRequestStatusFromCheckoutUserErrors(List<UserError> errors) {
            var payErrors = new List<ApplePayError>();
            var statusToReturn = ApplePayAuthorizationStatus.Failure;

            // Check to see if any of the user errors are not billing address errors
            // If it is not a billing address error return a failure
            foreach (var error in errors) {
                var fields = error.field();
                var fieldsSet = new HashSet<string>(fields);

                try {
                    string lastField = fields[fields.Count - 1];
                    ApplePayAddressInvalidError payError;

                    if (fieldsSet.Contains("billingAddress")) {
                        payError = new ApplePayBillingAddressInvalidError(error.message(), lastField);
                        statusToReturn = ApplePayAuthorizationStatus.InvalidBillingPostalAddress;
                    } else if (fieldsSet.Contains("shippingAddress")) {
                        payError = new ApplePayShippingAddressInvalidError(error.message(), lastField);
                        statusToReturn = ApplePayAuthorizationStatus.InvalidShippingPostalAddress;
                    } else {
                        statusToReturn = ApplePayAuthorizationStatus.Failure;
                        break;
                    }

                    payErrors.Add(payError);
                } catch {
                    statusToReturn = ApplePayAuthorizationStatus.Failure;
                    break;
                }
            }

            if (statusToReturn == ApplePayAuthorizationStatus.Failure) {
                return new UpdateRequestStatus(statusToReturn);
            } else {
                return new UpdateRequestStatus(statusToReturn, payErrors);
            }
        }

        private static UpdateRequestStatus GetUpdateRequestStatusFromShippingUserErrors(List<UserError> errors) {
            var statusToReturn = ApplePayAuthorizationStatus.Failure;
            var payErrors = new List<ApplePayError>();

            foreach (var error in errors) {
                var fields = error.field();

                try {
                    var lastField = fields[fields.Count - 1];
                    ApplePayError payError;

                    if (IsShippingAddressField(lastField)) {
                        payError = new ApplePayShippingAddressInvalidError(error.message(), lastField);
                        statusToReturn = ApplePayAuthorizationStatus.InvalidShippingPostalAddress;
                    } else if (IsContactField(lastField)) {
                        payError = new ApplePayContactInvalidError(error.message(), lastField);
                        statusToReturn = ApplePayAuthorizationStatus.InvalidShippingContact;
                    } else if (IsEmailField(lastField)) {
                        payError = new ApplePayContactInvalidError(error.message(), ApplePayContactInvalidError.ContactField.EmailAddress);
                        statusToReturn = ApplePayAuthorizationStatus.InvalidShippingContact;
                    } else {
                        statusToReturn = ApplePayAuthorizationStatus.Failure;
                        break;
                    }

                    payErrors.Add(payError);
                } catch {
                    statusToReturn = ApplePayAuthorizationStatus.Failure;
                    break;
                }
            }

            if (statusToReturn == ApplePayAuthorizationStatus.Failure) {
                return new UpdateRequestStatus(statusToReturn);
            } else {
                return new UpdateRequestStatus(statusToReturn, payErrors);
            }
        }

        // We only receive a partial shipping address before the user has authenticated
        // City, State, Zip, Country
        private static UpdateRequestStatus GetUpdateRequestStatusFromPreliminaryShippingUserErrors(List<UserError> errors) {
            var payErrors = new List<ApplePayError>();

            foreach (var error in errors) {
                var fields = error.field();

                try {
                    var lastField = fields[fields.Count - 1];

                    if (IsPartialShippingAddressField(lastField)) {
                        var payError = new ApplePayShippingAddressInvalidError(error.message(), lastField);
                        payErrors.Add(payError);
                    }
                } catch {
                    return new UpdateRequestStatus(ApplePayAuthorizationStatus.Failure);
                }
            }

            return new UpdateRequestStatus(ApplePayAuthorizationStatus.InvalidShippingPostalAddress, payErrors);
        }

        private void RespondToUpdateAddressSuccessForMessage(NativeMessage message) {
            var shippingMethods = GetShippingMethods();

            if (shippingMethods.Count > 0) {
                // Set the first shipping method as the default
                CartState.SetShippingLine(shippingMethods[0].Identifier, (ShopifyError error) => {
                    if (error == null) {
                        var summaryItems = GetSummaryItemsFromCheckout(CartState.CurrentCheckout);
                        message.Respond(new ApplePayEventResponse(ApplePayAuthorizationStatus.Success, summaryItems, shippingMethods).ToJsonString());
                    } else {
                        message.Respond(new ApplePayEventResponse(ApplePayAuthorizationStatus.Failure).ToJsonString());
                    }
                });
            } else {
                // Since there are no shipping methods available, it means that the shipping address that was set is unserviceable
                var summaryItems = GetSummaryItemsFromCheckout(CartState.CurrentCheckout);
                var payErrors = new List<ApplePayError>();
                payErrors.Add(new ApplePayShippingAddressUnservicableError("Shipping address is in an unserviceable area"));
                message.Respond(new ApplePayEventResponse(ApplePayAuthorizationStatus.InvalidShippingPostalAddress, summaryItems, shippingMethods, payErrors).ToJsonString());
            }
        }

        private void RespondToUpdateAddressErrorForMessage(NativeMessage message, ShopifyError error) {
            ApplePayEventResponse response = new ApplePayEventResponse(ApplePayAuthorizationStatus.Failure);

            // Check to see if this is a recoverable user error
            if (error.Type == ShopifyError.ErrorType.UserError) {
                var userErrors = CartState.UserErrors;

                var status = GetUpdateRequestStatusFromPreliminaryShippingUserErrors(userErrors);
                if (status.AuthorizationStatus == ApplePayAuthorizationStatus.InvalidShippingPostalAddress) {
                    var summaryItems = GetSummaryItemsFromCheckout(CartState.CurrentCheckout);
                    response =
                        new ApplePayEventResponse(
                            authorizationStatus: status.AuthorizationStatus,
                            summaryItems: summaryItems,
                            errors: status.Errors
                        );
                }
            }

            message.Respond(response.ToJsonString());
        }

        public void FetchApplePayCheckoutStatusForToken(string serializedMessage) {
            var checkout = CartState.CurrentCheckout;
            var message = NativeMessage.CreateFromJSON(serializedMessage);
            var paymentAmount = new MoneyInput(checkout.totalPrice(), checkout.currencyCode());
            var payment = new NativePayment(message.Content);
            var tokenizedPaymentInputV2 = new TokenizedPaymentInputV2(
              paymentAmount: paymentAmount,
              billingAddress: payment.BillingAddress,
              idempotencyKey: payment.TransactionIdentifier,
              paymentData: payment.PaymentData,
              type: "apple_pay"
            );

            Action performCheckout = () => {
                CheckoutWithTokenizedPaymentV2(tokenizedPaymentInputV2, checkout, (ApplePayEventResponse errorResponse) => {
                    if (errorResponse == null) {
                        message.Respond((new ApplePayEventResponse(ApplePayAuthorizationStatus.Success)).ToJsonString());
                    } else {
                        message.Respond(errorResponse.ToJsonString());
                    }
                });
            };

            SetFinalCheckoutFieldsForPayment(payment, checkout, (ApplePayEventResponse errorResponse) => {
                if (errorResponse == null) {
                    performCheckout();
                } else {
                    message.Respond(errorResponse.ToJsonString());
                }
            });
        }

        public void DidFinishCheckoutSession(string serializedMessage) {
            var message = NativeMessage.CreateFromJSON(serializedMessage);
            var paymentStatus = (NativePaymentStatus) Enum.Parse(typeof(NativePaymentStatus), (string) message.Content);

            switch (paymentStatus) {
                case NativePaymentStatus.Success:
                    OnSuccess();
                    return;
                case NativePaymentStatus.Cancelled:
                    OnCancelled();
                    return;
                case NativePaymentStatus.Failed:
                    var error = new ShopifyError(ShopifyError.ErrorType.NativePaymentProcessingError, "Unable to retrieve a payment from the user's payment provider. Fallback to web checkout.");
                    OnFailure(error);
                    return;
            }
        }

        private void SetFinalCheckoutFieldsForPayment(NativePayment payment, Checkout checkout, ApplePayEventHandlerCompletion callback) {
            ShippingFields? shippingFields = null;
            if (checkout.requiresShipping()) {
                shippingFields = new ShippingFields(payment.ShippingAddress, payment.ShippingIdentifier);
            }

            CartState.SetFinalCheckoutFields(payment.Email, shippingFields, (ShopifyError error) => {
                if (error == null) {
                    callback(null);
                } else {
                    ApplePayEventResponse response = new ApplePayEventResponse(ApplePayAuthorizationStatus.Failure);

                    // Check to see if this is a recoverable user error
                    if (error.Type == ShopifyError.ErrorType.UserError) {
                        var userErrors = CartState.UserErrors;
                        var status = GetUpdateRequestStatusFromShippingUserErrors(userErrors);

                        var summaryItems = GetSummaryItemsFromCheckout(CartState.CurrentCheckout);
                        if (status.AuthorizationStatus == ApplePayAuthorizationStatus.InvalidShippingPostalAddress) {
                            response =
                                new ApplePayEventResponse(
                                    authorizationStatus: status.AuthorizationStatus,
                                    summaryItems: summaryItems,
                                    errors: status.Errors
                                );
                        } else if (status.AuthorizationStatus == ApplePayAuthorizationStatus.InvalidShippingContact) {
                            response =
                                new ApplePayEventResponse(
                                    status.AuthorizationStatus,
                                    summaryItems,
                                    GetShippingMethods(),
                                    status.Errors
                                );
                        }
                    }

                    callback(response);
                }
            });
        }

        private void CheckoutWithTokenizedPaymentV2(TokenizedPaymentInputV2 tokenizedPaymentInputV2, Checkout checkout, ApplePayEventHandlerCompletion callback) {
            CartState.CheckoutWithTokenizedPaymentV2(tokenizedPaymentInputV2, (ShopifyError error) => {
                if (error == null) {
                    callback(null);
                } else {
                    ApplePayEventResponse response = new ApplePayEventResponse(ApplePayAuthorizationStatus.Failure);

                    // Check to see if this is a recoverable user error
                    if (error.Type == ShopifyError.ErrorType.UserError) {
                        var userErrors = CartState.UserErrors;
                        var status = GetUpdateRequestStatusFromCheckoutUserErrors(userErrors);

                        if (status.AuthorizationStatus != ApplePayAuthorizationStatus.Failure) {
                            var summaryItems = GetSummaryItemsFromCheckout(CartState.CurrentCheckout);
                            response = new ApplePayEventResponse(status.AuthorizationStatus, summaryItems, GetShippingMethods(), status.Errors);
                        }
                    }

                    callback(response);
                }
            });
        }
    }
}
#endif
