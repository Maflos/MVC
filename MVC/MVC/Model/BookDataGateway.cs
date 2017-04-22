using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MVC
{
    public class BookDataGateway : DataGateway
    {
        public BookDataGateway(string path) : base(path)
        {
        }

        public bool Insert(Book book)
        {
            bool isInserted = false;
            int id = 0;
            List<Book> bookList = FindAll();
            id = bookList[bookList.Count - 1].Id + 1;

            XDocument doc = XDocument.Load(path);
            XElement root = new XElement("book");
            root.Add(new XAttribute("id", id.ToString()));
            root.Add(new XElement("title", book.Title));
            root.Add(new XElement("author", book.Author));
            root.Add(new XElement("genre", book.Genre));
            root.Add(new XElement("quantity", book.Quantity.ToString()));
            root.Add(new XElement("price", book.Price.ToString()));
            doc.Element("books").Add(root);
            doc.Save(path);
            isInserted = true;

            return isInserted;
        }

        public Book Find(int id)
        {
            Book result = null;
            List<string> valueList = new List<string>();
            XmlTextReader reader = new XmlTextReader(path);
            bool isReady = false;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.HasAttributes)
                        {
                            if (int.Parse(reader.GetAttribute("id")) == id)
                            {
                                valueList.Add(reader.GetAttribute("id"));
                                isReady = true;
                            }
                        }
                        break;
                    case XmlNodeType.Text:
                        if (isReady)
                        {
                            valueList.Add(reader.Value);
                        }
                        break;
                    case XmlNodeType.EndElement:
                        break;
                }

                if (valueList.Count == 6)
                {
                    break;
                }
            }

            result = new Book(int.Parse(valueList[0]), valueList[1], valueList[2], 
                valueList[3], int.Parse(valueList[4]), double.Parse(valueList[5]));

            reader.Close();

            return result;
        }

        public bool Update(int id, Book book)
        {
            bool isUpdated = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            // find a node - here the one with given id
            XmlNode node = doc.SelectSingleNode("/books/book[@id=" + id + "]");

            // if found....
            if (node != null)
            {
                // get its child nodes
                XmlNodeList childList = node.ChildNodes;
                childList[0].InnerText = book.Title;
                childList[1].InnerText = book.Author;
                childList[2].InnerText = book.Genre;
                childList[3].InnerText = book.Quantity.ToString();
                childList[4].InnerText = book.Price.ToString();

                // save to file
                doc.Save(path);
                isUpdated = true;
            }

            return isUpdated;
        }

        public bool Delete(int id)
        {
            bool isDeleted = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            // find a node - here the one with given id
            XmlNode node = doc.SelectSingleNode("/books/book[@id=" + id + "]");

            // if found....
            if (node != null)
            {
                // get its parent node
                XmlNode parent = node.ParentNode;

                // remove the child node
                parent.RemoveChild(node);
          
                // save to file or whatever....
                doc.Save(path);
                isDeleted = true;
            }

            return isDeleted;
        }
       
        public List<Book> FindAll()
        {
            List<Book> result = new List<Book>();
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

            for (int i = 0; i < valueList.Count; i += 6)
            {
                Book b = new Book(int.Parse(valueList[i]), valueList[i + 1], valueList[i + 2],
                    valueList[i + 3], int.Parse(valueList[i + 4]), double.Parse(valueList[i + 5]));
                result.Add(b);
            }

            reader.Close();

            return result;
        }
    }
}
