# Microsoft Sql Server Express LocalDB

## Instances

LocalDB supports two kinds of instances: Automatic instances and named instances.


Automatic instances of LocalDB are public. 
They are created and managed automatically for the user and can be used by any application. 
One automatic instance of LocalDB exists for every version of LocalDB installed on the user's computer. 
Automatic instances of LocalDB provide seamless instance management. 
There is no need to create the instance; it just works. 
This feature allows for easy application installation and migration to a different computer. 
If the target machine has the specified version of LocalDB installed, the automatic instance of LocalDB for that version is available on the target machine as well. Automatic instances of LocalDB have a special pattern for the instance name that belongs to a reserved namespace. 
Automatic instances prevents name conflicts with named instances of LocalDB. 
The name for the automatic instance is `MSSQLLocalDB`.

Named instances of LocalDB are private. 
They are owned by a single application that is responsible for creating and managing the instance. 
Named instances provide isolation from other instances and can improve performance by reducing resource contention with other database users. 
Named instances must be created explicitly by the user through the LocalDB management API or implicitly via the app.config file for a managed application 
(although managed application may also use the API, if desired). 
Each named instance of LocalDB has an associated LocalDB version that points to the respective set of LocalDB binaries. 
The instance name of a LocalDB is sysname data type and can have up to 128 characters. 
(This differs from regular named instances of SQL Server, which limits names to regular NetBIOS names of 16 ASCII chars.) 
The name of an instance of LocalDB can contain any Unicode characters that are legal within a filename.
A named instance that uses an automatic instance name becomes an automatic instance.

Different users of a computer can have instances with the same name. Each instance is a different processes running as a different user.

## Shared instances of LocalDB

To share and unshared an instance of LocalDB, use the `LocalDBShareInstance` and `LocalDBUnShareInstance` methods of the LocalDB API, 
or the share and unshared options of the `SqlLocalDB` utility.

## Connecting

Connect to the automatic instance:

`Server=(localdb)\MSSQLLocalDB;Integrated Security=true`

To connect to a specific database by using the file name, connect using a connection string similar to:

`Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=D:\Data\MyDB1.mdf`

REM Create an instance of LocalDB
"C:\Program Files\Microsoft SQL Server\130\Tools\Binn\SqlLocalDB.exe" create LocalDBApp1

REM Start the instance of LocalDB
"C:\Program Files\Microsoft SQL Server\130\Tools\Binn\SqlLocalDB.exe" start LocalDBApp1

REM Gather information about the instance of LocalDB
"C:\Program Files\Microsoft SQL Server\130\Tools\Binn\SqlLocalDB.exe" info LocalDBApp1

To connect to a shared instance of LocalDB add `\.\` (backslash + dot + backslash) to the connection string to reference the namespace reserved for shared instances. 
For example, to connect to a shared instance of LocalDB named AppData use a connection string such as `(localdb)\.\AppData` as part of the connection string. 
A user connecting to a shared instance of LocalDB that they do not own must have a Windows Authentication or SQL Server Authentication login.


# Reference

SQL Server Express LocalDB
https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15

https://expressdb.io/sql-server-express-vs-localdb.html#what-is-localdb
