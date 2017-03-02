using System;
using System.Xml;
using XmlNodeVisualizer;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = new XmlDocument();
            var xml = @"<CURRENCY>
            <CurrencyID>1</CurrencyID>
            <Code>EUR</Code>
            <Description>Euro</Description>
            <ExchangeRate>1.000</ExchangeRate>
            <Format>0.00</Format>
            <Sign>€</Sign>
            <DMWCode>EUR</DMWCode>
            <DMWExchangeRate>1.000</DMWExchangeRate>
            <DMWExchangeFromDollar>1.000</DMWExchangeFromDollar>
            <HtmlSign>&amp;euro;</HtmlSign>
            <GiftCardIssuanceLimit>1000.000</GiftCardIssuanceLimit>
            </CURRENCY>";
            doc.LoadXml(xml);
            XmlVisualizer.TestShowVisualizer(doc);
            Console.ReadLine();
        }
    }
}
