namespace Shopify.Unity {
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Shopify.Unity.SDK;

    /// <summary>
    /// ISO 3166-1 alpha-2 country codes with some differences.
    /// </summary>
    public enum CountryCode {
        /// <summary>
        /// If the SDK is not up to date with the schema in the Storefront API, it is possible
        /// to have enum values returned that are unknown to the SDK. In this case the value
        /// will actually be UNKNOWN.
        /// </summary>
        UNKNOWN,
        /// <summary>
        /// Andorra.
        /// </summary>
        AD,
        /// <summary>
        /// United Arab Emirates.
        /// </summary>
        AE,
        /// <summary>
        /// Afghanistan.
        /// </summary>
        AF,
        /// <summary>
        /// Antigua And Barbuda.
        /// </summary>
        AG,
        /// <summary>
        /// Anguilla.
        /// </summary>
        AI,
        /// <summary>
        /// Albania.
        /// </summary>
        AL,
        /// <summary>
        /// Armenia.
        /// </summary>
        AM,
        /// <summary>
        /// Netherlands Antilles.
        /// </summary>
        AN,
        /// <summary>
        /// Angola.
        /// </summary>
        AO,
        /// <summary>
        /// Argentina.
        /// </summary>
        AR,
        /// <summary>
        /// Austria.
        /// </summary>
        AT,
        /// <summary>
        /// Australia.
        /// </summary>
        AU,
        /// <summary>
        /// Aruba.
        /// </summary>
        AW,
        /// <summary>
        /// Aland Islands.
        /// </summary>
        AX,
        /// <summary>
        /// Azerbaijan.
        /// </summary>
        AZ,
        /// <summary>
        /// Bosnia And Herzegovina.
        /// </summary>
        BA,
        /// <summary>
        /// Barbados.
        /// </summary>
        BB,
        /// <summary>
        /// Bangladesh.
        /// </summary>
        BD,
        /// <summary>
        /// Belgium.
        /// </summary>
        BE,
        /// <summary>
        /// Burkina Faso.
        /// </summary>
        BF,
        /// <summary>
        /// Bulgaria.
        /// </summary>
        BG,
        /// <summary>
        /// Bahrain.
        /// </summary>
        BH,
        /// <summary>
        /// Burundi.
        /// </summary>
        BI,
        /// <summary>
        /// Benin.
        /// </summary>
        BJ,
        /// <summary>
        /// Saint Barthélemy.
        /// </summary>
        BL,
        /// <summary>
        /// Bermuda.
        /// </summary>
        BM,
        /// <summary>
        /// Brunei.
        /// </summary>
        BN,
        /// <summary>
        /// Bolivia.
        /// </summary>
        BO,
        /// <summary>
        /// Bonaire, Sint Eustatius and Saba.
        /// </summary>
        BQ,
        /// <summary>
        /// Brazil.
        /// </summary>
        BR,
        /// <summary>
        /// Bahamas.
        /// </summary>
        BS,
        /// <summary>
        /// Bhutan.
        /// </summary>
        BT,
        /// <summary>
        /// Bouvet Island.
        /// </summary>
        BV,
        /// <summary>
        /// Botswana.
        /// </summary>
        BW,
        /// <summary>
        /// Belarus.
        /// </summary>
        BY,
        /// <summary>
        /// Belize.
        /// </summary>
        BZ,
        /// <summary>
        /// Canada.
        /// </summary>
        CA,
        /// <summary>
        /// Cocos (Keeling) Islands.
        /// </summary>
        CC,
        /// <summary>
        /// Congo, The Democratic Republic Of The.
        /// </summary>
        CD,
        /// <summary>
        /// Central African Republic.
        /// </summary>
        CF,
        /// <summary>
        /// Congo.
        /// </summary>
        CG,
        /// <summary>
        /// Switzerland.
        /// </summary>
        CH,
        /// <summary>
        /// Côte d'Ivoire.
        /// </summary>
        CI,
        /// <summary>
        /// Cook Islands.
        /// </summary>
        CK,
        /// <summary>
        /// Chile.
        /// </summary>
        CL,
        /// <summary>
        /// Republic of Cameroon.
        /// </summary>
        CM,
        /// <summary>
        /// China.
        /// </summary>
        CN,
        /// <summary>
        /// Colombia.
        /// </summary>
        CO,
        /// <summary>
        /// Costa Rica.
        /// </summary>
        CR,
        /// <summary>
        /// Cuba.
        /// </summary>
        CU,
        /// <summary>
        /// Cape Verde.
        /// </summary>
        CV,
        /// <summary>
        /// Curaçao.
        /// </summary>
        CW,
        /// <summary>
        /// Christmas Island.
        /// </summary>
        CX,
        /// <summary>
        /// Cyprus.
        /// </summary>
        CY,
        /// <summary>
        /// Czech Republic.
        /// </summary>
        CZ,
        /// <summary>
        /// Germany.
        /// </summary>
        DE,
        /// <summary>
        /// Djibouti.
        /// </summary>
        DJ,
        /// <summary>
        /// Denmark.
        /// </summary>
        DK,
        /// <summary>
        /// Dominica.
        /// </summary>
        DM,
        /// <summary>
        /// Dominican Republic.
        /// </summary>
        DO,
        /// <summary>
        /// Algeria.
        /// </summary>
        DZ,
        /// <summary>
        /// Ecuador.
        /// </summary>
        EC,
        /// <summary>
        /// Estonia.
        /// </summary>
        EE,
        /// <summary>
        /// Egypt.
        /// </summary>
        EG,
        /// <summary>
        /// Western Sahara.
        /// </summary>
        EH,
        /// <summary>
        /// Eritrea.
        /// </summary>
        ER,
        /// <summary>
        /// Spain.
        /// </summary>
        ES,
        /// <summary>
        /// Ethiopia.
        /// </summary>
        ET,
        /// <summary>
        /// Finland.
        /// </summary>
        FI,
        /// <summary>
        /// Fiji.
        /// </summary>
        FJ,
        /// <summary>
        /// Falkland Islands (Malvinas).
        /// </summary>
        FK,
        /// <summary>
        /// Faroe Islands.
        /// </summary>
        FO,
        /// <summary>
        /// France.
        /// </summary>
        FR,
        /// <summary>
        /// Gabon.
        /// </summary>
        GA,
        /// <summary>
        /// United Kingdom.
        /// </summary>
        GB,
        /// <summary>
        /// Grenada.
        /// </summary>
        GD,
        /// <summary>
        /// Georgia.
        /// </summary>
        GE,
        /// <summary>
        /// French Guiana.
        /// </summary>
        GF,
        /// <summary>
        /// Guernsey.
        /// </summary>
        GG,
        /// <summary>
        /// Ghana.
        /// </summary>
        GH,
        /// <summary>
        /// Gibraltar.
        /// </summary>
        GI,
        /// <summary>
        /// Greenland.
        /// </summary>
        GL,
        /// <summary>
        /// Gambia.
        /// </summary>
        GM,
        /// <summary>
        /// Guinea.
        /// </summary>
        GN,
        /// <summary>
        /// Guadeloupe.
        /// </summary>
        GP,
        /// <summary>
        /// Equatorial Guinea.
        /// </summary>
        GQ,
        /// <summary>
        /// Greece.
        /// </summary>
        GR,
        /// <summary>
        /// South Georgia And The South Sandwich Islands.
        /// </summary>
        GS,
        /// <summary>
        /// Guatemala.
        /// </summary>
        GT,
        /// <summary>
        /// Guinea Bissau.
        /// </summary>
        GW,
        /// <summary>
        /// Guyana.
        /// </summary>
        GY,
        /// <summary>
        /// Hong Kong.
        /// </summary>
        HK,
        /// <summary>
        /// Heard Island And Mcdonald Islands.
        /// </summary>
        HM,
        /// <summary>
        /// Honduras.
        /// </summary>
        HN,
        /// <summary>
        /// Croatia.
        /// </summary>
        HR,
        /// <summary>
        /// Haiti.
        /// </summary>
        HT,
        /// <summary>
        /// Hungary.
        /// </summary>
        HU,
        /// <summary>
        /// Indonesia.
        /// </summary>
        ID,
        /// <summary>
        /// Ireland.
        /// </summary>
        IE,
        /// <summary>
        /// Israel.
        /// </summary>
        IL,
        /// <summary>
        /// Isle Of Man.
        /// </summary>
        IM,
        /// <summary>
        /// India.
        /// </summary>
        IN,
        /// <summary>
        /// British Indian Ocean Territory.
        /// </summary>
        IO,
        /// <summary>
        /// Iraq.
        /// </summary>
        IQ,
        /// <summary>
        /// Iran, Islamic Republic Of.
        /// </summary>
        IR,
        /// <summary>
        /// Iceland.
        /// </summary>
        IS,
        /// <summary>
        /// Italy.
        /// </summary>
        IT,
        /// <summary>
        /// Jersey.
        /// </summary>
        JE,
        /// <summary>
        /// Jamaica.
        /// </summary>
        JM,
        /// <summary>
        /// Jordan.
        /// </summary>
        JO,
        /// <summary>
        /// Japan.
        /// </summary>
        JP,
        /// <summary>
        /// Kenya.
        /// </summary>
        KE,
        /// <summary>
        /// Kyrgyzstan.
        /// </summary>
        KG,
        /// <summary>
        /// Cambodia.
        /// </summary>
        KH,
        /// <summary>
        /// Kiribati.
        /// </summary>
        KI,
        /// <summary>
        /// Comoros.
        /// </summary>
        KM,
        /// <summary>
        /// Saint Kitts And Nevis.
        /// </summary>
        KN,
        /// <summary>
        /// Korea, Democratic People's Republic Of.
        /// </summary>
        KP,
        /// <summary>
        /// South Korea.
        /// </summary>
        KR,
        /// <summary>
        /// Kuwait.
        /// </summary>
        KW,
        /// <summary>
        /// Cayman Islands.
        /// </summary>
        KY,
        /// <summary>
        /// Kazakhstan.
        /// </summary>
        KZ,
        /// <summary>
        /// Lao People's Democratic Republic.
        /// </summary>
        LA,
        /// <summary>
        /// Lebanon.
        /// </summary>
        LB,
        /// <summary>
        /// Saint Lucia.
        /// </summary>
        LC,
        /// <summary>
        /// Liechtenstein.
        /// </summary>
        LI,
        /// <summary>
        /// Sri Lanka.
        /// </summary>
        LK,
        /// <summary>
        /// Liberia.
        /// </summary>
        LR,
        /// <summary>
        /// Lesotho.
        /// </summary>
        LS,
        /// <summary>
        /// Lithuania.
        /// </summary>
        LT,
        /// <summary>
        /// Luxembourg.
        /// </summary>
        LU,
        /// <summary>
        /// Latvia.
        /// </summary>
        LV,
        /// <summary>
        /// Libyan Arab Jamahiriya.
        /// </summary>
        LY,
        /// <summary>
        /// Morocco.
        /// </summary>
        MA,
        /// <summary>
        /// Monaco.
        /// </summary>
        MC,
        /// <summary>
        /// Moldova, Republic of.
        /// </summary>
        MD,
        /// <summary>
        /// Montenegro.
        /// </summary>
        ME,
        /// <summary>
        /// Saint Martin.
        /// </summary>
        MF,
        /// <summary>
        /// Madagascar.
        /// </summary>
        MG,
        /// <summary>
        /// Macedonia, Republic Of.
        /// </summary>
        MK,
        /// <summary>
        /// Mali.
        /// </summary>
        ML,
        /// <summary>
        /// Myanmar.
        /// </summary>
        MM,
        /// <summary>
        /// Mongolia.
        /// </summary>
        MN,
        /// <summary>
        /// Macao.
        /// </summary>
        MO,
        /// <summary>
        /// Martinique.
        /// </summary>
        MQ,
        /// <summary>
        /// Mauritania.
        /// </summary>
        MR,
        /// <summary>
        /// Montserrat.
        /// </summary>
        MS,
        /// <summary>
        /// Malta.
        /// </summary>
        MT,
        /// <summary>
        /// Mauritius.
        /// </summary>
        MU,
        /// <summary>
        /// Maldives.
        /// </summary>
        MV,
        /// <summary>
        /// Malawi.
        /// </summary>
        MW,
        /// <summary>
        /// Mexico.
        /// </summary>
        MX,
        /// <summary>
        /// Malaysia.
        /// </summary>
        MY,
        /// <summary>
        /// Mozambique.
        /// </summary>
        MZ,
        /// <summary>
        /// Namibia.
        /// </summary>
        NA,
        /// <summary>
        /// New Caledonia.
        /// </summary>
        NC,
        /// <summary>
        /// Niger.
        /// </summary>
        NE,
        /// <summary>
        /// Norfolk Island.
        /// </summary>
        NF,
        /// <summary>
        /// Nigeria.
        /// </summary>
        NG,
        /// <summary>
        /// Nicaragua.
        /// </summary>
        NI,
        /// <summary>
        /// Netherlands.
        /// </summary>
        NL,
        /// <summary>
        /// Norway.
        /// </summary>
        NO,
        /// <summary>
        /// Nepal.
        /// </summary>
        NP,
        /// <summary>
        /// Nauru.
        /// </summary>
        NR,
        /// <summary>
        /// Niue.
        /// </summary>
        NU,
        /// <summary>
        /// New Zealand.
        /// </summary>
        NZ,
        /// <summary>
        /// Oman.
        /// </summary>
        OM,
        /// <summary>
        /// Panama.
        /// </summary>
        PA,
        /// <summary>
        /// Peru.
        /// </summary>
        PE,
        /// <summary>
        /// French Polynesia.
        /// </summary>
        PF,
        /// <summary>
        /// Papua New Guinea.
        /// </summary>
        PG,
        /// <summary>
        /// Philippines.
        /// </summary>
        PH,
        /// <summary>
        /// Pakistan.
        /// </summary>
        PK,
        /// <summary>
        /// Poland.
        /// </summary>
        PL,
        /// <summary>
        /// Saint Pierre And Miquelon.
        /// </summary>
        PM,
        /// <summary>
        /// Pitcairn.
        /// </summary>
        PN,
        /// <summary>
        /// Palestinian Territory, Occupied.
        /// </summary>
        PS,
        /// <summary>
        /// Portugal.
        /// </summary>
        PT,
        /// <summary>
        /// Paraguay.
        /// </summary>
        PY,
        /// <summary>
        /// Qatar.
        /// </summary>
        QA,
        /// <summary>
        /// Reunion.
        /// </summary>
        RE,
        /// <summary>
        /// Romania.
        /// </summary>
        RO,
        /// <summary>
        /// Serbia.
        /// </summary>
        RS,
        /// <summary>
        /// Russia.
        /// </summary>
        RU,
        /// <summary>
        /// Rwanda.
        /// </summary>
        RW,
        /// <summary>
        /// Saudi Arabia.
        /// </summary>
        SA,
        /// <summary>
        /// Solomon Islands.
        /// </summary>
        SB,
        /// <summary>
        /// Seychelles.
        /// </summary>
        SC,
        /// <summary>
        /// Sudan.
        /// </summary>
        SD,
        /// <summary>
        /// Sweden.
        /// </summary>
        SE,
        /// <summary>
        /// Singapore.
        /// </summary>
        SG,
        /// <summary>
        /// Saint Helena.
        /// </summary>
        SH,
        /// <summary>
        /// Slovenia.
        /// </summary>
        SI,
        /// <summary>
        /// Svalbard And Jan Mayen.
        /// </summary>
        SJ,
        /// <summary>
        /// Slovakia.
        /// </summary>
        SK,
        /// <summary>
        /// Sierra Leone.
        /// </summary>
        SL,
        /// <summary>
        /// San Marino.
        /// </summary>
        SM,
        /// <summary>
        /// Senegal.
        /// </summary>
        SN,
        /// <summary>
        /// Somalia.
        /// </summary>
        SO,
        /// <summary>
        /// Suriname.
        /// </summary>
        SR,
        /// <summary>
        /// South Sudan.
        /// </summary>
        SS,
        /// <summary>
        /// Sao Tome And Principe.
        /// </summary>
        ST,
        /// <summary>
        /// El Salvador.
        /// </summary>
        SV,
        /// <summary>
        /// Sint Maarten.
        /// </summary>
        SX,
        /// <summary>
        /// Syria.
        /// </summary>
        SY,
        /// <summary>
        /// Swaziland.
        /// </summary>
        SZ,
        /// <summary>
        /// Turks and Caicos Islands.
        /// </summary>
        TC,
        /// <summary>
        /// Chad.
        /// </summary>
        TD,
        /// <summary>
        /// French Southern Territories.
        /// </summary>
        TF,
        /// <summary>
        /// Togo.
        /// </summary>
        TG,
        /// <summary>
        /// Thailand.
        /// </summary>
        TH,
        /// <summary>
        /// Tajikistan.
        /// </summary>
        TJ,
        /// <summary>
        /// Tokelau.
        /// </summary>
        TK,
        /// <summary>
        /// Timor Leste.
        /// </summary>
        TL,
        /// <summary>
        /// Turkmenistan.
        /// </summary>
        TM,
        /// <summary>
        /// Tunisia.
        /// </summary>
        TN,
        /// <summary>
        /// Tonga.
        /// </summary>
        TO,
        /// <summary>
        /// Turkey.
        /// </summary>
        TR,
        /// <summary>
        /// Trinidad and Tobago.
        /// </summary>
        TT,
        /// <summary>
        /// Tuvalu.
        /// </summary>
        TV,
        /// <summary>
        /// Taiwan.
        /// </summary>
        TW,
        /// <summary>
        /// Tanzania, United Republic Of.
        /// </summary>
        TZ,
        /// <summary>
        /// Ukraine.
        /// </summary>
        UA,
        /// <summary>
        /// Uganda.
        /// </summary>
        UG,
        /// <summary>
        /// United States Minor Outlying Islands.
        /// </summary>
        UM,
        /// <summary>
        /// United States.
        /// </summary>
        US,
        /// <summary>
        /// Uruguay.
        /// </summary>
        UY,
        /// <summary>
        /// Uzbekistan.
        /// </summary>
        UZ,
        /// <summary>
        /// Holy See (Vatican City State).
        /// </summary>
        VA,
        /// <summary>
        /// St. Vincent.
        /// </summary>
        VC,
        /// <summary>
        /// Venezuela.
        /// </summary>
        VE,
        /// <summary>
        /// Virgin Islands, British.
        /// </summary>
        VG,
        /// <summary>
        /// Vietnam.
        /// </summary>
        VN,
        /// <summary>
        /// Vanuatu.
        /// </summary>
        VU,
        /// <summary>
        /// Wallis And Futuna.
        /// </summary>
        WF,
        /// <summary>
        /// Samoa.
        /// </summary>
        WS,
        /// <summary>
        /// Kosovo.
        /// </summary>
        XK,
        /// <summary>
        /// Yemen.
        /// </summary>
        YE,
        /// <summary>
        /// Mayotte.
        /// </summary>
        YT,
        /// <summary>
        /// South Africa.
        /// </summary>
        ZA,
        /// <summary>
        /// Zambia.
        /// </summary>
        ZM,
        /// <summary>
        /// Zimbabwe.
        /// </summary>
        ZW
    }
    }
