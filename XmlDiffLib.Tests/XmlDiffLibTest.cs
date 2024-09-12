using System.Xml;

namespace XmlDiffLib.Tests
{
    public class Tests
    {
        [Test]
        public void CheckCompare()
        {
            XmlTestResources.ResourceManager.GetStream("");
            
            XmlDiff diff = new XmlDiff(XmlTestResources.HAAR01000, XmlTestResources.HAAR01001, "HAAR01000", "HAAR01001");
            XmlDiffOptions options = new XmlDiffOptions();
            options.IgnoreAttributeOrder = true;
            options.IgnoreAttributes = false;
            options.MaxAttributesToDisplay = 3;
            diff.CompareDocuments(options);
            Assert.AreEqual(diff.DiffNodeList.Count, 0);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(XmlTestResources.HAAR01001);
            XmlNode node = xdoc.SelectSingleNode("//Product");
            node.Attributes["EffDate"].Value = "01/01/3000";
            diff = new XmlDiff(XmlTestResources.HAAR01000, xdoc.InnerXml, "HAAR01000", "HAAR01001");
            diff.CompareDocuments(options);
            Assert.AreEqual(diff.DiffNodeList.Count, 2);
            Assert.IsTrue(diff.DiffNodeList[1].Description.Contains("EffDate"));
            xdoc.LoadXml(XmlTestResources.HAAR01001);
            node = xdoc.SelectSingleNode("//Company");
            node.Attributes["CompanyID"].Value = "1000";
            diff = new XmlDiff(XmlTestResources.HAAR01000, xdoc.InnerXml);
            diff.CompareDocuments(options);
            Assert.AreEqual(diff.DiffNodeList.Count, 2);
            Assert.IsTrue(diff.DiffNodeList[1].Description.Contains("CompanyID"));
        }
    }
}