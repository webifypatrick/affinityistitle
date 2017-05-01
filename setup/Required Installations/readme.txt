The components in this directory are required for the affinity application.  If you are 
asked for confirmation to register in the Global Assembly Cache, you must select "Yes"

MySql.Data
----------

This is the connection driver to allow .NET to communicate with a MySQL server.

5.0.3 is the original driver used when the app was first released
6.1.2 has been tested on more current versions of MySQL
(Use 6.1.2 if you get "Object Not Initialized" Errors when logging in

If 6.1.2 is used, the assembly string must be updated in Web.Config (See Web.Config.DEFAULT)


ASP AJAX
--------

AJAX has been incorporated into ASP.NET so this install is not needed on more recent
machines, however if necessary it can be installed manually