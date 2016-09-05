<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)
or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());
$cant=$_POST["cantidad"];
$query = mysql_query("select * from Preguntas where Nivel  = \"" . $_POST["numeroNivel"] . "\" ORDER BY RAND() LIMIT ".$cant." ")or die("Error en la query" . mysql_error());
$salida="";
while($resultQuery = mysql_fetch_array($query)){
	$salida=$salida.$resultQuery['Pregunta'].'*'.$resultQuery['Respuesta'].'*'.$resultQuery['Distractor1'].'*'.$resultQuery['Distractor2'].'*'.$resultQuery['Distractor3'].'|';
};
echo $salida;
?>