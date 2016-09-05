<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)
or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());

$query = mysql_query("select Password from Alumno where IdAlumno = \"" . $_POST["username"] . "\"")
or die("Unable to validate login and password with the database:" . mysql_error());

$resultQuery = mysql_fetch_array($query);
$id=$resultQuery[1];
$result = $resultQuery[0];
$parts = explode( ':', $result);
     // Check if password is md5-ed with or without salt
    if (count($parts) < 2) $new_password = md5($_POST["password"]); 
    else {
       $salt = $parts[1];
       // convert the raw password to md5(password+salt):salt model
       $new_password = md5($_POST["password"] . $salt) . ":" . $salt; 
    } 
    if ($new_password != $result){
    	echo "datos incorrectos";  // your params
    }
    else{
      // echo  "correcto";//$pne=obtenerPuntajeNivelExp($id, $_POST["idJuego"]);
       echo "correcto";  // your params
    }
?>