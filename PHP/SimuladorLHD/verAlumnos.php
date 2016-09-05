<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)
or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());
if($_POST["id"]=="todos"){
	$query = mysql_query("select a.Nombre , a.IdAlumno from Alumno a")or die("Error en la query" . mysql_error());
	$salida="";
	while($resultQuery = mysql_fetch_array($query)){
	  $salida=$salida.$resultQuery['IdAlumno']."|".$resultQuery['Nombre']."*";
	};
}
else{
	$query = mysql_query("select a.Nombre , a.IdAlumno from Alumno a, Administrador_Alumno b where b.IdAlumno=a.IdAlumno and b.IdAdministrador  = \"" . $_POST["id"] . "\"")
	or die("Error en la query" . mysql_error());
	$salida="";
	while($resultQuery = mysql_fetch_array($query)){
	  $salida=$salida.$resultQuery['IdAlumno']."|".$resultQuery['Nombre']."*";
	};
}
echo $salida;
?>