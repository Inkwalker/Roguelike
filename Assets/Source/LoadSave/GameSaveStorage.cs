using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Roguelike.LoadSave
{
    public class GameSaveStorage
    {
        public GameSaveStorage()
        {

        }

        public void Write(GameSaveSlot gameSave)
        {
            var json = JsonUtility.ToJson(gameSave, false);

            using (var file = OpenFile("Save/slot_0.sav"))
            {
                if (file.Length > 0)
                    file.SetLength(0);

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(file, gameSave);

                Debug.Log("Save file size: " + file.Length / 1024 + "KB");
            }
        }

        public GameSaveSlot Read()
        {
            GameSaveSlot gameSave;

            using (var file = OpenFile("Save/slot_0.sav"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                gameSave = (GameSaveSlot)formatter.Deserialize(file);
            }

            return gameSave;
        }

        private FileStream OpenFile(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            fileInfo.Directory.Create();

            FileStream file = File.Open(fileName, FileMode.OpenOrCreate);

            //if (file.Length == 0) file.SetLength(sectorSize);

            return file;
        }
    }
}
