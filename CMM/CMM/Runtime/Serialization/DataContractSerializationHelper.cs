namespace CMM.Runtime.Serialization
{
    using CMM.IO;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;

    public class DataContractSerializationHelper
    {
        public static T Deserialize<T>(string filePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            return (T) Deserialize(typeof(T), new Type[] { typeof(T) }, filePath);
        }

        public static object Deserialize(Type type, IEnumerable<Type> knownTypes, Stream stream)
        {
            DataContractSerializer serializer = new DataContractSerializer(type, knownTypes);
            return serializer.ReadObject(stream);
        }

        public static object Deserialize(Type type, IEnumerable<Type> knownTypes, string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Deserialize(type, knownTypes, stream);
            }
        }

        public static void Serialize<T>(T o, Stream stream)
        {
            new DataContractSerializer(typeof(T)).WriteObject(stream, o);
        }

        public static void Serialize<T>(T o, string filePath)
        {
            IOUtility.EnsureDirectoryExists(Path.GetDirectoryName(filePath));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                Serialize<T>(o, stream);
            }
        }
    }
}

