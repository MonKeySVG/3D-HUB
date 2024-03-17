using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Interpreter : MonoBehaviour
{
    Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        { "black", "#021b21" },
        { "gray", "#555d71" },
        { "red", "#ff5879" },
        { "yellow", "#f2f1b9" },
        { "blue", "#9ed9d8" },
        { "purple", "#d926ff" },
        { "orange", "#ef5847" }
    };

    List<String> response = new List<string>();



    
    public List<String> Interpret(string userInput) {

        response.Clear();

        string[] args = userInput.Split();

        


        
        if (SceneController.instance.originScene == "Level 0") {
            if (args[0] == "help") {
                ListEntry("help", "return a list of commands");
                ListEntry("ls", "Get the list of all the files");
                ListEntry("dl", "Download a file");
                return response;
            }

            if (args[0] == "dl") {


                if (args.Length >= 2) {
                    if (args[1] == "note.txt") {
                        string url = "https://perso.esiee.fr/~belghitr/Cybersecurity-Game/Challenges/Challenge1/note.txt";
                        DownloadManager DownloadManager = gameObject.GetComponent<DownloadManager>();
                        DownloadManager.DownloadURLfrom(url);

                        response.Add("Opération réussie");
                        return response;
                    }  else if (args[1] == "confidential.zip") {
                        string url = "https://perso.esiee.fr/~belghitr/Cybersecurity-Game/Challenges/Challenge1/confidential.zip";
                        DownloadManager DownloadManager = gameObject.GetComponent<DownloadManager>();
                        DownloadManager.DownloadURLfrom(url);

                        response.Add("Opération réussie");
                        return response;
                    }else {
                        response.Add("Le fichier spécifié n'éxiste pas");
                        return response;

                    }
                } else {
                    response.Add("Veuillez spécifier un fichier");
                    return response;
                }
                
                
            }

            if (args[0] == "ls") {
                response.Add("      note.txt");
                response.Add("      confidential.zip");

                return response;
            }



            else {
                UnknownCommand();

                return response;
            }

        
        }

        if (SceneController.instance.originScene == "Level 1") {
            if (args[0] == "help") {
                ListEntry("help", "return a list of commands");
                ListEntry("getcode", "Get the secret code");

                return response;
            }

            if (args[0] == "getcode") {
                ListEntry("Code", "1234");
                return response;
            }



            else {
                UnknownCommand();

                return response;
            }

        
        }

        else {
            UnknownCommand();

            return response;
        }

    }


    public string ColorString(string s, string color) {
        string leftTag = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }

    void ListEntry(string a, string b) {
        response.Add(ColorString(a, colors["orange"]) + ": " + ColorString(b, colors["yellow"]));
    }

    void UnknownCommand () {
        response.Add("Command not recognized. Type \"help\" for a list of commands.");
    }

    public List<String> LoadTitle(string path, string color, int spacing) {

        List<String> title = new List<string>();


        

        try {
            StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));

            for (int i=0; i<spacing; i++) {
                title.Add("");
            }

            while(!file.EndOfStream) {
                title.Add(ColorString(file.ReadLine(), colors[color]));
            }

            for (int i=0; i<spacing; i++) {
                title.Add("");
            }

            file.Close();
        } catch (Exception e) {
            Debug.LogError("Error loading title: " + e.Message);
        }

        return title;
    }

    public List<String> LoadSubtitle(string color, int spacing) {

        List<String> subtitle = new List<string>();

        for (int i=0; i<spacing; i++) {
            subtitle.Add("");
        }

        subtitle.Add("Accès autorisé. Système d'exploitation en cours d'initialisation.");
        subtitle.Add("Chargement des protocoles sécurisés en cours...");
        

        subtitle.Add("");
        subtitle.Add("---------------------------");
        subtitle.Add("");

        subtitle.Add("Système Opérationnel en Ligne");

        subtitle.Add("");
        subtitle.Add("---------------------------");
        subtitle.Add("");

        subtitle.Add(ColorString("Accès hautement restreint.", "red"));
        subtitle.Add("Toute tentative d'accès non autorisée sera détectée et traitée conformément aux protocoles de sécurité en vigueur.");
        
        subtitle.Add("");
        subtitle.Add("Pour toute assistance technique, contacter l'administration de la base.");


        subtitle.Add("");
        subtitle.Add("Accès autorisé aux utilisateurs du département H34C uniquement.");
        subtitle.Add("Toute diffusion d'informations classifiées est strictement interdite.");

        for (int i=0; i<spacing; i++) {
            subtitle.Add("");
        }

        return subtitle;
                

        // List<String> coloredSubtitle = new List<string>();

        // for (int i=0; i<spacing; i++) {
        //     coloredSubtitle.Add("");
        // }

        // foreach (string subtitleLine in subtitle) {
        //     coloredSubtitle.Add(ColorString(subtitleLine, colors[color]));
        // }

        // for (int i=0; i<spacing; i++) {
        //     coloredSubtitle.Add("");
        // }
     

        // return coloredSubtitle;
    }
}
