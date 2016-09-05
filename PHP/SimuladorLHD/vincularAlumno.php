<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)
or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());
$idalumno=$_POST["idAlumno"];
$idadmin=$_POST["idAdmin"];

$query = mysql_query("INSERT INTO Administrador_Alumno(IdAlumno, IdAdministrador) values( \"" .$idalumno. "\", \"" .$idadmin. "\")")or die("Error en la query" . mysql_error());


echo "correcto";
?>