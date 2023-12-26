using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace CustomTool.SafeVariables {
    /// <summary> Esta clase guarda y carga varios tipos de datos y clases usando encriptación AES en playerprefs. </summary>
    public static class SafeEncrypt {
        // ==================== VARIABLES ===================
        private static string key = "%D*G-KaPdSgVkYp3s6v8y/B?E(H+MbQe";

        /// <summary> La clave de cifrado.
        /// Usa siempre la misma clave al descifrar, de lo contrario saldrá un error. </summary>
        public static string Key {
            get => key;
            set {
                if (value.Length == 32 || value.Length == 16) key = value;
            }
        }

        // ==================== METODOS ====================
        /// <summary>
        /// Encripta y guarda un int en playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void EncryptInt(this int _value, string _prefKey) {
            Int _i = new() { x = _value };
            EncryptClass(_i, _prefKey);
        }

        /// <summary>
        /// Descifra y carga un int desde playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static int DecryptInt(string _prefKey, int _defaultValue = default) {
            Int _i = DecryptClass<Int>(_prefKey);
            return _i != null ? _i.x : _defaultValue;
        }

        /// <summary>
        /// Encripta y guarda un bool en playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void EncryptBool(this bool _value, string _prefKey) {
            Bool _bl = new() { b = _value };
            EncryptClass(_bl, _prefKey);
        }

        /// <summary>
        /// Descifra y carga un bool desde playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static bool DecryptBool(string _prefKey, bool _defaultValue = default) {
            Bool _bl = DecryptClass<Bool>(_prefKey);
            return _bl != null ? _bl.b : _defaultValue;
        }

        /// <summary>
        /// Encripta y guarda un string en playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void EncryptString(this string _value, string _prefKey) {
            String _st = new() { s = _value };
            EncryptClass(_st, _prefKey);
        }

        /// <summary>
        /// Descifra y carga un string desde playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static string DecryptString(string _prefKey, string _defaultValue = default) {
            String _st = DecryptClass<String>(_prefKey);
            return _st != null ? _st.s : _defaultValue;
        }

        /// <summary>
        /// Encripta y guarda un float en playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void EncryptFloat(this float _value, string _prefKey) {
            Float _fl = new() { f = _value };
            EncryptClass(_fl, _prefKey);
        }

        /// <summary>
        /// Descifra y carga un float desde playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static float DecryptFloat(string _prefKey, float _defaultValue = default) {
            Float _fl = DecryptClass<Float>(_prefKey);
            return _fl != null ? _fl.f : _defaultValue;
        }

        /// <summary>
        /// Encripta y guarda un char en playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void EncryptChar(this char _value, string _prefKey) {
            Char _ch = new() { c = _value };
            EncryptClass(_ch, _prefKey);
        }

        /// <summary>
        /// Descifra y carga un char desde playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static char DecryptChar(string _prefKey, char _defaultValue = default) {
            Char _ch = DecryptClass<Char>(_prefKey);
            return _ch != null ? _ch.c : _defaultValue;
        }


        /// <summary>
        /// Encripta y guarda un long en playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void EncryptLong(this long _value, string _prefKey) {
            Long _lg = new() { l = _value };
            EncryptClass(_lg, _prefKey);
        }

        /// <summary>
        /// Descifra y carga un long desde playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static long DecryptLong(string _prefKey, long _defaultValue = default) {
            Long _lg = DecryptClass<Long>(_prefKey);
            return _lg != null ? _lg.l : _defaultValue;
        }

        /// <summary>
        /// Encripta y guarda un DateTime en playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void EncryptDateTime(this DateTime _value, string _prefKey) {
            Long _lg = new() { l = _value.Ticks };
            EncryptClass(_lg, _prefKey);
        }

        /// <summary>
        /// Descifra y carga un DateTime desde playerprefs.
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static DateTime DecryptDateTime(string _prefKey, DateTime _defaultValue = default) {
            Long _lg = DecryptClass<Long>(_prefKey);
            DateTime _dt = _lg != null ? new(_lg.l) : _defaultValue;

            return _dt;
        }

        /// <summary>
        /// Encripta y guarda los campos públicos de una clase (debe ser Serializable) en playerprefs. 
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void EncryptClass<T>(this T _obj, string _prefKey) where T : class {
            string _data = EncryptText(JsonUtility.ToJson(_obj));
            PlayerPrefs.SetString(_prefKey, _data);
        }

        /// <summary>
        /// Descifra y carga los campos públicos de una clase (debe ser Serializable) desde playerprefs (no usar con MonoBehaviour o ScriptableObject, usar DecryptClassOverwrite en su lugar).
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static T DecryptClass<T>(string _prefKey) where T : class {
            if (PlayerPrefs.HasKey(_prefKey)) {
                string _data = DecryptText(PlayerPrefs.GetString(_prefKey));
                return JsonUtility.FromJson<T>(_data);
            }

            return null;
        }

        /// <summary>
        /// Descifra y carga los campos públicos de una clase (debe ser Serializable) desde playerprefs (no usar con MonoBehaviour o ScriptableObject, usar DecryptClassOverwrite en su lugar).
        /// Nota: Usará una clave por defecto si la propiedad Key no está establecida.
        /// </summary>
        public static void DecryptClassOverwrite<T>(T _obj, string _prefKey) {
            if (PlayerPrefs.HasKey(_prefKey)) {
                string _data = DecryptText(PlayerPrefs.GetString(_prefKey));
                JsonUtility.FromJsonOverwrite(_data, _obj);
            }
        }

        /// <summary>
        /// Devuelve 'true' si la prefKey que se le da existe.
        /// </summary>
        public static bool HasPrefKey(string _prefKey) => PlayerPrefs.HasKey(_prefKey);

        /// <summary>
        /// Migra el valor almacenado de una key a otra. 
        /// Esto puede ser útil si se desea cambiar el nombre de una key de PlayerPrefs,
        /// pero sin que se pierdan los datos existentes almacenados en la key antigua.
        /// </summary>
        /// <param name="_oldKey">La key antigua de la que se quiere mover los datos.</param>
        /// <param name="_newKey">La nueva key a la que se le quieren pasar los datos.</param>
        /// <param name="_deleteOldKey">Si se quiere eliminar o no la key antigua.</param>
        /// <returns> Devuelve 'true' si la operación de migración se ha completado correctamente. </returns>
        public static bool MigratePrefKey(string _oldKey, string _newKey, bool _deleteOldKey = true) {
            if (HasPrefKey(_oldKey)) {
                string _oldKeyValue = PlayerPrefs.GetString(_oldKey);
                PlayerPrefs.SetString(_newKey, _oldKeyValue);

                if (_deleteOldKey) DeletePrefKey(_oldKey);
                return true;
            }

            return false;
        }

        /// <summary> Elimina una prefKey y su valor correspondiente. </summary>
        public static void DeletePrefKey(string _prefKey) => PlayerPrefs.DeleteKey(_prefKey);

        /// <summary> Elimina todas las prefKeys y todos sus valores correspondientes. </summary>
        public static void DeleteAllPrefKeys() => PlayerPrefs.DeleteAll();

        private static string EncryptText(string _plainText) {
            if (key.Length != 32 && key.Length != 16) {
                Debug.LogError("Specified key is not a valid size for this algorithm. \n" + "Make sure it is a 128bit or 256bit string");
                return "";
            }

            try {
                byte[] _iv = new byte[16];
                byte[] _array;

                using (Aes aes = Aes.Create()) {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = _iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using MemoryStream memoryStream = new();
                    using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                    using (StreamWriter streamWriter = new(cryptoStream)) {
                        streamWriter.Write(_plainText);
                    }

                    _array = memoryStream.ToArray();
                }

                return Convert.ToBase64String(_array);

            } catch (Exception _ex) {
                Debug.LogError(_ex.Message);
                return "";
            }
        }

        public static string DecryptText(string _cipherText) {
            try {
                if (_cipherText.Length > 0) {
                    byte[] _iv = new byte[16];
                    byte[] _buffer = Convert.FromBase64String(_cipherText);

                    using Aes aes = Aes.Create();
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = _iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using MemoryStream memoryStream = new(_buffer);
                    using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
                    using StreamReader streamReader = new(cryptoStream);

                    return streamReader.ReadToEnd();
                }
                
                return "";

            } catch(Exception _ex) {
                Debug.LogError(_ex.Message);
                return "";
            }
        }

        [Serializable]
        class Int {
            public int x;
        }

        [Serializable]
        class Bool {
            public bool b;
        }

        [Serializable]
        class String {
            public string s;
        }

        [Serializable]
        class Float {
            public float f;
        }

        [Serializable]
        class Char {
            public char c;
        }

        [Serializable]
        class Long {
            public long l;
        }
    }
}