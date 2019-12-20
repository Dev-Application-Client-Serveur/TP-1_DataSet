using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Mode_Deconnecte_SqlCommandBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string connexionString;
            connexionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=GestionStagiaires;User= 'sa'; password = 'admintp4'";
            SqlConnection connexion = new SqlConnection(connexionString);
            SqlCommand cmdSelect = connexion.CreateCommand();
            cmdSelect.CommandText = "Select * from stagiaires";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmdSelect;
            DataSet ds = new DataSet();
            da.Fill(ds);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);

            // Ajouter un stagiaire
            DataRow dr1 = ds.Tables[0].NewRow();
            dr1["id"] = 3;
            dr1["nom"] = "Ali";
            ds.Tables[0].Rows.Add(dr1);
            da.Update(ds);
            DataRow dr2 = ds.Tables[0].NewRow();
            dr2["id"] = 4;
            dr2["nom"] = "Karim";
            ds.Tables[0].Rows.Add(dr2);
            da.Update(ds);

            // Update 
            DataRow drKarim = ds.Tables[0].Rows[0];
            drKarim["nom"] = "Moad";
            da.Update(ds);

            // Supprimer le premièr stagiaire
            DataRow drStagiaire = ds.Tables[0].Rows[0];
            drStagiaire.Delete();
            da.Update(ds);

            // Afficher tous les stagiaires
            foreach (DataRow ligne in ds.Tables[0].Rows)
            {
                Console.WriteLine("ID : {0} | Nom : {1} | Prenom : {2}", ligne[0], ligne[1], ligne["Prenom"]);
                Console.WriteLine("\n");
            }

            Console.ReadLine();
        }
    }
}
