using UnityEngine;

namespace Assets.Script.Classes
{
    public class ClassesManager
    {
        private static ClassesManager instance;

        public static ClassesManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClassesManager();
                }
                return instance;
            }
        }

        // Store every class
        private readonly ClasseCromagnon classeCromagnon = ScriptableObject.CreateInstance<ClasseCromagnon>();
        private readonly ClassePtérodactyle classePtérodactyle = ScriptableObject.CreateInstance<ClassePtérodactyle>();
        private readonly ClasseSamourai classeSamourai = ScriptableObject.CreateInstance<ClasseSamourai>();
        private readonly ClasseFuturiste classeFuturiste = ScriptableObject.CreateInstance<ClasseFuturiste>();

        // Expose the class
        public static ClasseCromagnon Cromagnon => Instance.classeCromagnon;
        public static ClassePtérodactyle Ptérodactyle => Instance.classePtérodactyle;
        public static ClasseSamourai Samourai => Instance.classeSamourai;
        public static ClasseFuturiste Futuriste => Instance.classeFuturiste;

        public static ClasseBase GetClasse(TypeClasse typeClasse)
        {
            return typeClasse switch
            {
                TypeClasse.Cromagnon => Cromagnon,
                TypeClasse.Ptérodactyle => Ptérodactyle,
                TypeClasse.Samourai => Samourai,
                TypeClasse.Futuriste => Futuriste,
                _ => throw new System.Exception("Classe not found")
            };
        }

        public static ClasseBase[] GetClasses()
        {
            return new ClasseBase[] {
                Cromagnon,
                Ptérodactyle,
                Samourai,
                Futuriste
            };
        }

        public static TypeClasse[] GetPlayerClasses()
        {
            return new TypeClasse[]
            {
                TypeClasse.Cromagnon,
                TypeClasse.Samourai,
                TypeClasse.Futuriste
            };
        }
    }
}
