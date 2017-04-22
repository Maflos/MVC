using MVC.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC.Controller
{
    public class LoginController
    {
        private LoginView loginView;

        public LoginController(LoginView loginView)
        {
            this.loginView = loginView;
            loginView.SetLoginController(this);
        }

        public bool Login(string username, string password)
        {
            if (username == "" || password == "")
            {
                return false;
            }
            else
            {
                UserDataGateway udg = new UserDataGateway("user.xml");
                List<User> userList = udg.FindAll();
                User currentUser = null;

                foreach(User usr in userList)
                {
                    if(usr.Name == username && usr.Password == password)
                    {
                        currentUser = usr;
                    }
                }

                if (currentUser == null)
                {
                    return false;
                }
                else if (currentUser.IsAdmin)
                {
                    Admin adminView = new Admin(currentUser);
                    AdminController aCont = new AdminController(adminView);
                    adminView.Show();
                }
                else
                {
                    RegularUser regUserView = new RegularUser(currentUser);
                    RegularUserController regUserController = new RegularUserController(regUserView);
                    regUserView.Show();
                }
            }

            return true;
        }

    }
}
