using PModelo.Models;
using PModelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;

namespace PModelo.Helpers
{
    public class UserHelper
    {
        private static Random random;

        public static User GetRandomUser()
        {
            return Users[random.Next(0, Users.Count)];
        }


        public static ObservableCollection<Grouping<string, User>> UsersGrouped { get; set; }

        public static ObservableCollection<User> Users { get; set; }

        public static ParqueaderoItemViewModel Parqueadero { get; set; }

        static UserHelper()
        {
            random = new Random();
            Users = new ObservableCollection<User>();
            Parqueadero = new ParqueaderoItemViewModel();
            LoadUserGroup();
           
            var sorted = from  user in Users
                         orderby user.FirstName
                         group user by user.NameSort into userGroup
                         select new Grouping<string, User>(userGroup.Key, userGroup);

            UsersGrouped = new ObservableCollection<Grouping<string, User>>(sorted);

        }

        private static void LoadUserGroup()
        {
            Users.Add(new User { FirstName = "Aaron", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Jefe", Email = "AronAlvarez@hotmail.com", Picture = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg" });

            Users.Add(new User { FirstName = "Luis", LastName = "Alvarez", MotherLastName = "Caballero", DNI = "11200666", Cargo = "Jefe", Email = "Luisperso_015@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" });

            Users.Add(new User { FirstName = "Juan Ramon", LastName = "Riveiro", MotherLastName = "Carrillo", DNI = "099200033", Cargo = "Rol0", Email = "JuanRamon@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" });

            Users.Add(new User { FirstName = "Ruben Dario", LastName = "Cuba", MotherLastName = "Postmodernismo", DNI = "11200055", Cargo = "Rol0", Email = "RubenDario@hotmail.com", Picture = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg" });

            Users.Add(new User { FirstName = "Juan Ramon", LastName = "Riveiro", MotherLastName = "Carrillo", DNI = "099200033", Cargo = "Rol0", Email = "JuanRamon@hotmail.com", Picture = "http://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Proboscis_Monkey_in_Borneo.jpg/250px-Proboscis_Monkey_in_Borneo.jpg" });

            Users.Add(new User { FirstName = "Aaron", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Jefe", Email = "AronAlvarez@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" });

            Users.Add(new User { FirstName = "Zaroastro", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol1", Email = "AronAlvarez@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" });

            Users.Add(new User { FirstName = "Jose Maria", LastName = "Euguren", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol3", Email = "AronAlvarez@hotmail.com", Picture = "http://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Mandrill_at_san_francisco_zoo.jpg/220px-Mandrill_at_san_francisco_zoo.jpg" });
        }
    }
}



// new User { FirstName = "Aaron", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Jefe", Email = "AronAlvarez@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/23c1dd13-333a-459e-9e23-c3784e7cb434/2016-06-02_1049.png" },
//new User { FirstName = "Luis", LastName = "Alvarez", MotherLastName = "Caballero", DNI = "11200666", Cargo = "Jefe", Email = "Luisperso_015@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" },
//new User { FirstName = "Juan Ramon", LastName = "Riveiro", MotherLastName = "Carrillo", DNI = "099200033", Cargo = "Rol0", Email = "JuanRamon@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" },
//new User { FirstName = "Ruben Dario", LastName = "Cuba", MotherLastName = "Postmodernismo", DNI = "11200055", Cargo = "Rol0", Email = "RubenDario@hotmail.com" },
//new User { FirstName = "Juan Ramon", LastName = "Riveiro", MotherLastName = "Carrillo", DNI = "099200033", Cargo = "Rol0", Email = "JuanRamon@hotmail.com" },
//new User { FirstName = "Aaron", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Jefe", Email = "AronAlvarez@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" },
//new User { FirstName = "Ruben Dario", LastName = "Cuba", MotherLastName = "Postmodernismo", DNI = "11200055", Cargo = "Rol0", Email = "RubenDario@hotmail.com" },
//new User { FirstName = "Zaroastro", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol1", Email = "AronAlvarez@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" },
//new User { FirstName = "Jhon Lenon", LastName = "Alvarez", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol2", Email = "AronAlvarez@hotmail.com" },
//new User { FirstName = "Jose Maria", LastName = "Euguren", MotherLastName = "Bartra", DNI = "11200033", Cargo = "Rol3", Email = "AronAlvarez@hotmail.com", Picture = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png" },



//Users.Add(new User
//{
//    FirstName = "Baboon",
//    Cargo = "Africa & Asia",
//    //Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
//    Picture = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
//});

//Users.Add(new User
//{
//    FirstName= "Capuchin Monkey",
//    Cargo= "Central & South America",
//    //Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
//    Picture = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
//});

//Users.Add(new User
//{
//    FirstName = "Blue Monkey",
//    Cargo = "Central and East Africa",
//    //Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
//    Picture= "http://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
//});


//Users.Add(new User
//{
//    FirstName = "Squirrel Monkey",
//    Cargo = "Central & South America",
//    //Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.",
//    Picture= "http://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
//});

//Users.Add(new User
//{
//    FirstName= "Golden Lion Tamarin",
//    Cargo = "Brazil",
//    //Details = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
//    Picture = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Golden_lion_tamarin_portrait3.jpg/220px-Golden_lion_tamarin_portrait3.jpg"
//});

//Users.Add(new User
//{
//    FirstName = "Howler Monkey",
//    Cargo = "South America",
//    //Details = "Howler monkeys are among the largest of the New World monkeys. Fifteen species are currently recognised. Previously classified in the family Cebidae, they are now placed in the family Atelidae.",
//    Picture= "http://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Alouatta_guariba.jpg/200px-Alouatta_guariba.jpg"
//});

//Users.Add(new User
//{
//    FirstName = "Japanese Macaque",
//    Cargo= "Japan",
//    //Details = "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey because they live in areas where snow covers the ground for months each",
//    Picture= "http://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Macaca_fuscata_fuscata1.jpg/220px-Macaca_fuscata_fuscata1.jpg"
//});

//Users.Add(new User
//{
//    FirstName = "Mandrill",
//    Cargo= "Southern Cameroon, Gabon, Equatorial Guinea, and Congo",
//    //Details = "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill. It is found in southern Cameroon, Gabon, Equatorial Guinea, and Congo.",
//    Picture= "http://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Mandrill_at_san_francisco_zoo.jpg/220px-Mandrill_at_san_francisco_zoo.jpg"
//});

//Users.Add(new User
//{
//    FirstName = "Proboscis Monkey",
//    Cargo = "Borneo",
//    //Details = "The proboscis monkey or long-nosed monkey, known as the bekantan in Malay, is a reddish-brown arboreal Old World monkey that is endemic to the south-east Asian island of Borneo.",
//    Picture= "http://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Proboscis_Monkey_in_Borneo.jpg/250px-Proboscis_Monkey_in_Borneo.jpg"
//});
