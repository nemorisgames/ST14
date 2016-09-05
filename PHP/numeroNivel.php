<?php
include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)
or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());

$query = mysql_query("select * from Nivel where IdNivel  = \"" . $_POST["id"] . "\"")
or die("Error en la query" . mysql_error());
$salida="";
while($resultQuery = mysql_fetch_array($query)){
  $salida=$salida.$resultQuery['NumeroNivel'].'*'.$resultQuery['TiempoVuelta'].'*'.$resultQuery['TiempoFaena'].'*'.$resultQuery['tiempoExtMin'].'*'.$resultQuery['tiempoExtMax'].'*'.$resultQuery['CantidadVueltas'].'*'.$resultQuery['ChoqueZipper'].'*'.$resultQuery['AreaExtraccion'].'*'.$resultQuery['CamionMercedes'].'*'.$resultQuery['IntPosterior'].'*'.$resultQuery['IntPosteriorDer'].'*'.$resultQuery['IntPosteriorIzq'].'*'.$resultQuery['IntMedioDer'].'*'.$resultQuery['IntCabina'].'*'.$resultQuery['IntBrazo'].'*'.$resultQuery['ExitoPreguntas'].'*'.$resultQuery['CantPreguntas'].'*'.$resultQuery['minimoCargar'].'*'.$resultQuery['maximoCargar'].'*'.$resultQuery['TonelajeALlevar'].'*'.$resultQuery['CaidaPermitida'].'*'.$resultQuery['descuentoChoque'].'*'.$resultQuery['checklist1'].'*'.$resultQuery['checklist2'].'*'.$resultQuery['descuentoTunel'].'*'.$resultQuery['descuentoCamion'];
};
echo $salida;
?>