<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)
or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());

$query = mysql_query("SELECT count(*) as repeticiones FROM Historial_Alumno h_a, Historial h, Nivel n Where h_a.IdHistorial = h.IdHistorial and h.IdNivel = n.IdNivel and n.NumeroNivel = '" . $_POST["numeroNivel"] . "' and h_a.IdAlumno = '" . $_POST["idAlumno"] . "'")
or die("-1");
$salida="";
while($resultQuery = mysql_fetch_array($query)){
  $salida=$resultQuery['repeticiones'];
};
echo $salida;
?>