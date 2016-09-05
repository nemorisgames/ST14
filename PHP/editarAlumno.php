<?php
$idAdmin=$_POST['id'];
$password=$_POST['pass'];


include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());
$validar=mysql_query("Select IdAlumno from Alumno Where IdAlumno=\"".$idAdmin."\"") or die("-1");
//aqui ver si existe y devolver

$query0=mysql_query("Update Alumno set  Nombre=\"". $_POST["nombre"]."\", Password=\"".md5($password)."\", Sexo=\"". $_POST["sexo"]."\", Mail=\"". $_POST["mail"]."\" Where IdAlumno=\"".$idAdmin."\"") or die("-2");



echo "correcto";
?>