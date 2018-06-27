// automatically generated, do not modify

using FlatBuffers;

public sealed class User : Table {
  public static User GetRootAsUser(ByteBuffer _bb) { return GetRootAsUser(_bb, new User()); }
  public static User GetRootAsUser(ByteBuffer _bb, User obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public User __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Userid { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public string Name { get { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; } }
  public string Password { get { int o = __offset(8); return o != 0 ? __string(o + bb_pos) : null; } }

  public static Offset<User> CreateUser(FlatBufferBuilder builder,
      int userid = 0,
      StringOffset name = default(StringOffset),
      StringOffset password = default(StringOffset)) {
    builder.StartObject(3);
    User.AddPassword(builder, password);
    User.AddName(builder, name);
    User.AddUserid(builder, userid);
    return User.EndUser(builder);
  }

  public static void StartUser(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddUserid(FlatBufferBuilder builder, int userid) { builder.AddInt(0, userid, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(1, nameOffset.Value, 0); }
  public static void AddPassword(FlatBufferBuilder builder, StringOffset passwordOffset) { builder.AddOffset(2, passwordOffset.Value, 0); }
  public static Offset<User> EndUser(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<User>(o);
  }
};

public sealed class DataBase : Table {
  public static DataBase GetRootAsDataBase(ByteBuffer _bb) { return GetRootAsDataBase(_bb, new DataBase()); }
  public static DataBase GetRootAsDataBase(ByteBuffer _bb, DataBase obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public static bool DataBaseBufferHasIdentifier(ByteBuffer _bb) { return __has_identifier(_bb, "DATA"); }
  public DataBase __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public int Port { get { int o = __offset(4); return o != 0 ? bb.GetInt(o + bb_pos) : (int)0; } }
  public string IpAdress { get { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; } }
  public User GetUserList(int j) { return GetUserList(new User(), j); }
  public User GetUserList(User obj, int j) { int o = __offset(8); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int UserListLength { get { int o = __offset(8); return o != 0 ? __vector_len(o) : 0; } }

  public static Offset<DataBase> CreateDataBase(FlatBufferBuilder builder,
      int port = 0,
      StringOffset ipAdress = default(StringOffset),
      VectorOffset userList = default(VectorOffset)) {
    builder.StartObject(3);
    DataBase.AddUserList(builder, userList);
    DataBase.AddIpAdress(builder, ipAdress);
    DataBase.AddPort(builder, port);
    return DataBase.EndDataBase(builder);
  }

  public static void StartDataBase(FlatBufferBuilder builder) { builder.StartObject(3); }
  public static void AddPort(FlatBufferBuilder builder, int port) { builder.AddInt(0, port, 0); }
  public static void AddIpAdress(FlatBufferBuilder builder, StringOffset ipAdressOffset) { builder.AddOffset(1, ipAdressOffset.Value, 0); }
  public static void AddUserList(FlatBufferBuilder builder, VectorOffset userListOffset) { builder.AddOffset(2, userListOffset.Value, 0); }
  public static VectorOffset CreateUserListVector(FlatBufferBuilder builder, Offset<User>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartUserListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<DataBase> EndDataBase(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<DataBase>(o);
  }
  public static void FinishDataBaseBuffer(FlatBufferBuilder builder, Offset<DataBase> offset) { builder.Finish(offset.Value, "DATA"); }
};

