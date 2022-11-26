// ReSharper disable All
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SL_SaveBinary<T>
{
    #region Methods
    
    public void Save(T _toSave, string _path)
    {
        Stream _file = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        IFormatter _toBinary = new BinaryFormatter();
        _toBinary.Serialize(_file, _toSave);
        _file.Dispose();
    }
    public T Load(string _path)
    {
        Stream _file = new FileStream(_path, FileMode.Open, FileAccess.Read);
        IFormatter _toObject = new BinaryFormatter();
        T _object = (T)_toObject.Deserialize(_file);
        _file.Dispose();
        return _object;
    }
    
    #endregion
}