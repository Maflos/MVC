using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MVC
{
    public class UserDataGateway : DataGateway
    {
        public UserDataGateway(string path) : base(path)
        {
        }

        public bool Insert(User user)
        {
            bool isInserted = false;
            int id = 0;
            List<User> userList = FindAll();
            id = userList[userList.Count - 1].Id + 1;

            XDocument doc = XDocument.Load(path);
            XElement root = new XElement("user");
            root.Add(new XAttribute("id", id.ToString()));
            root.Add(new XElement("name", user.Name));
            root.Add(new XElement("email", user.Email));
            root.Add(new XElement("password", user.Password));
            root.Add(new XElement("admin", user.IsAdmin.ToString()));
            doc.Element("users").Add(root);
            doc.Save(path);
            isInserted = true;

            return isInserted;
        }

        public User Find(int id)
        {
            User result = null;
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

                if (valueList.Count == 5)
                {
                    break;
                }
            }

            result = new User(int.Parse(valueList[0]), valueList[1], valueList[2],
                valueList[3], bool.Parse(valueList[4]));

            reader.Close();

            return result;
        }

        public bool Update(int id, User user)
        {
            bool isUpdated = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            // find a node - here the one with given id
            XmlNode node = doc.SelectSingleNode("/users/user[@id=" + id + "]");

            // if found....
            if (node != null)
            {
                // get its child nodes
                XmlNodeList childList = node.ChildNodes;
                childList[0].InnerText = user.Name;
                childList[1].InnerText = user.Email;
                childList[2].InnerText = user.Password;
                childList[3].InnerText = user.IsAdmin.ToString();

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
            XmlNode node = doc.SelectSingleNode("/users/user[@id=" + id + "]");

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

        public List<User> FindAll()
        {
            List<User> result = new List<User>();
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
                User usr = new User(int.Parse(valueList[i]), valueList[i + 1], valueList[i + 2],
                    valueList[i + 3], bool.Parse(valueList[i + 4]));
                result.Add(usr);
            }

            reader.Close();

            return result;
        }
    }
}
