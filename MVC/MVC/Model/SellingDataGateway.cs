using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MVC
{
    public class SellingDataGateway : DataGateway
    {
        public SellingDataGateway(string path) : base(path)
        {
        }

        public bool Insert(Selling selling)
        {
            bool isInserted = false;

            XDocument doc = XDocument.Load(path);
            XElement root = new XElement("selling");
            root.Add(new XAttribute("id", selling.SellingID.ToString()));
            root.Add(new XElement("seller", selling.UserID.ToString()));
            root.Add(new XElement("book", selling.BookID.ToString()));
            root.Add(new XElement("quantity", selling.Quantity.ToString()));
            root.Add(new XElement("price", selling.TotalPrice.ToString()));
            doc.Element("sellings").Add(root);
            doc.Save(path);
            isInserted = true;

            return isInserted;
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            // find a node - here the one with given id
            XmlNode node = doc.SelectSingleNode("/sellings/selling[@id=" + id + "]");

            // if found....
            if (node != null)
            {
                // get its parent node
                XmlNode parent = node.ParentNode;

                // remove the child node
                parent.RemoveChild(node);

                doc.Save(path);
                isDeleted = true;
            }

            return isDeleted;
        }

        public List<Selling> FindAll()
        {
            List<Selling> result = new List<Selling>();
            List<string> valueList = new List<string>();
            XmlTextReader reader = new XmlTextReader(path);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.HasAttributes)
                        {
                            valueList.Add(reader.GetAttribute("id"));
                        }
                        break;
                    case XmlNodeType.Text:
                        valueList.Add(reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        break;
                }
            }

            for (int i = 0; i < valueList.Count; i += 5)
            {
                Selling selling = new Selling(int.Parse(valueList[i]), int.Parse(valueList[i + 1]), int.Parse(valueList[i + 2]),
                    int.Parse(valueList[i + 3]), double.Parse(valueList[i + 4]));

                result.Add(selling);
            }

            reader.Close();

            return result;
        }
    }
}
