<?php

function conectar(){
	$link = mysql_connect('localhost', 'nemorisg_LHD', 'Simulador1');
	if (!$link) {
		die('Not connected : ' . mysql_error());
	}
	$db_selected = mysql_select_db('nemorisg_simuladorLHD', $link);
	if (!$db_selected) {
		die ('Can\'t use DB : ' . mysql_error());
	}
	mysql_set_charset('utf8');
	//return $db_selected;
}
conectar();

function ejecutarQuery($query){
	$result = mysql_query($query);
	if (!$result) {
		$message  = 'Invalid query: ' . mysql_error() . "\n";
		$message .= 'Whole query: ' . $query;
		die($message);
	}
	return $result;
}
if($_GET['operacion'] != ""){
switch($_GET['operacion']){
	case 0: buscarImagen($_POST['username']); break;
	case 1: cambiarInteres($_POST['username'], $_POST['edad'], $_POST['interes'], $_POST['imagen']); break;
	case 2: traerInformacion($_POST['username']); break;
	//case 1: actualizarPrecio($_POST['idFarmacia'], $_POST['idProducto'], $_POST['precio']); break;
}
}

function traerInformacion($username){
	$query=ejecutarQuery("select nombre, edad, sexo, interes from usuario where username = \"".$username."\"");
	$result = mysql_fetch_array($query);
	if($result[0] == "") echo -1;
	echo $result[0]."|".$result[1]."|".$result[2]."|".$result[3];
}

function cambiarInteres($username, $edad, $interes, $imagen){
	$result=ejecutarQuery("update usuario set interes=".$interes.", foto = '".$imagen."', edad = ".$edad." where username='".$username."'");
}

function buscarImagen($username){
	$query=ejecutarQuery("select foto from usuario where username = \"".$username."\"");
	$result = mysql_fetch_array($query);
	if($result[0] == "") echo -1;
	echo $result[0];
}

/*function actualizarPrecio($idFarmacia, $idProducto, $precio){
	$result=ejecutarQuery("update FarmaciaProducto set precio=".$precio." where fk_farmacia=".$idFarmacia." and fk_producto=".$idProducto);
}



function solicitarFarmacias($buscar){
	$retorno = "{\"farmacias\": [";
	$result=ejecutarQuery("SELECT far.pk_farmacia, far.nombre, far.direccion, far.latitud, far.longitud, c.nombre as nombreComuna, r.nombre as nombreRegion, p.pk_producto, f.nombre as nombreProducto, f_p.precio, p.dosis, du.nombre as nombreDosis, p.presentacion, pu.nombre as nombrePresentacion, m.nombre as nombreMarca FROM Comuna c, Region r, Farmacia far, FarmaciaProducto f_p, Producto p, Familia f, DosisUnidad du, PresentacionUnidad pu, Marca m where far.fk_comuna = c.pk_comuna and c.fk_region = r.pk_region and f_p.fk_producto = p.pk_producto and f_p.fk_farmacia = far.pk_farmacia and p.fk_familia = f.pk_familia and p.fk_dosisUnidad = du.pk_dosisUnidad and p.fk_presentacionUnidad = pu.pk_presentacionUnidad and f.fk_marca = m.pk_marca and f.nombre like '%$buscar%'");
	$farmaciaIdActual = -1;
	while ($row = mysql_fetch_assoc($result)) {
		if($farmaciaIdActual != $row['pk_farmacia']){
			if($farmaciaIdActual != -1) $retorno .= "]},";
			$farmaciaIdActual = $row['pk_farmacia'];
			$retorno .= "{
						\"id\":$farmaciaIdActual,
	  					\"nombre\": \"".$row['nombre']."\",
						\"direccion\": \"".$row['direccion']."\",
						\"latitud\": \"".$row['latitud']."\",
						\"longitud\": \"".$row['longitud']."\",
						\"comuna\": \"".$row['nombreComuna']."\",
						\"region\": \"".$row['nombreRegion']."\",
						\"productos\": [";
		}
		else{
			$retorno .= ",";
		}
		$retorno .= "{ \"id\": ".$row['pk_producto'].",
		  \"nombre\": \"".$row['nombreProducto']."\",
		  \"precio\": ".$row['precio'].",
		  \"dosis\": \"".$row['dosis']." ".$row['nombreDosis']."\",
		  \"presentacion\": \"".$row['presentacion']." ".$row['nombrePresentacion']."\",
		  \"marca\": \"".$row['nombreMarca']."\" }";
	}
	$retorno .= "]}]}";
	mysql_free_result($result);
	echo $retorno;
}

function globalUsers($nRows, $actual){
	$result=ejecutarQuery("select count(*) as quantity, DATE_FORMAT(registerDate, '%d/%M/%Y') as date from user group by DATE_FORMAT(registerDate, '%d/%M/%Y') limit ".($actual*$nRows).",".$nRows);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['quantity']."</td><td>".$row['date']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);	
}

function globalGold(){
	$result=ejecutarQuery("select sum(totalGold) as total, sum(totalGold)-sum(gold) as spend from user group by null");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['total']."</td><td>".$row['spend']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);	
}

function obtenerSceneSessionWeb($nRows, $actual){
	$result=ejecutarQuery("select u.username, s.date, s.session_time, s.gold_collected_session from session s, user u where s.fk_user=u.pk_user order by s.date desc limit ".($actual*$nRows).",".$nRows);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['username']."</td><td>".$row['date']."</td><td>".$row['session_time']."</td><td>".$row['gold_collected_session']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerEventCountWeb($event){
	$result=ejecutarQuery("select description, count(*) as quantity from log_AFM where type='".$event."' group by description");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['description']."</td><td>".$row['quantity']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerStopPlayingSceneWeb(){
	$result=ejecutarQuery("select last_scene_played, count(*) as quantity from session group by last_scene_played order by quantity desc");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['last_scene_played']."</td><td>".$row['quantity']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerStartPlayingWeb($nRows, $actual){
	$result=ejecutarQuery("select username, registerDate from user order by registerDate desc limit ".($actual*$nRows).",".$nRows);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['username']."</td><td>".$row['registerDate']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerCardQuantityWeb($nRows, $actual){
	$result=ejecutarQuery("select b.name, count(*) as quantity from card_user bu, card b where b.pk_card=bu.fk_card group by b.name limit ".($actual*$nRows).",".$nRows);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['name']."</td><td>".$row['quantity']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerItemQuantityWeb($item, $nRows, $actual){
	$result=ejecutarQuery("select b.name, count(*) as quantity, b.priceGold, b.priceDiamond from ".$item."_user bu, $item b where b.pk_$item=bu.fk_$item group by b.name limit ".($actual*$nRows).",".$nRows);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['name']."</td><td>".$row['quantity']."</td><td>".$row['priceGold']."</td><td>".$row['priceDiamond']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerItemBuyedWeb($item, $nRows, $actual){
	$result=ejecutarQuery("select u.username, c.name, cu.purchaseDate from ".$item."_user cu, user u, $item c where cu.fk_user=u.pk_user and c.pk_$item=cu.fk_$item ORDER BY purchaseDate DESC limit ".($actual*$nRows).",".$nRows);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		//$linkUsuarios="<a href='juego_user.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkLogros="<a href='logros.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkItems="<a href='items.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$tabla.="<tr><td>".$row['username']."</td><td>".$row['name']."</td><td>".$row['purchaseDate']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
}

function actualizarSchoolLevelWeb($levelSchoolAbilities, $priceGold, $priceDiamond){
	$result=ejecutarQuery("update levelSchoolAbilities set priceGold=$priceGold, priceDiamond=$priceDiamond where pk_levelSchoolAbilities=$levelSchoolAbilities");
}

function actualizarSchoolWeb($idSchool, $name){
	$result=ejecutarQuery("update schoolAbility set name='$name' where pk_schoolAbility=$idSchool");
}

function obtenerSchoolWeb($id){
	$result=ejecutarQuery("select * from schoolAbility where pk_schoolAbility=$id");
	$result2=ejecutarQuery("select * from levelSchoolAbilities where fk_schoolAbility=$id");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.=
		"Name          : <input type='text' name='name' value='".$row['name']."'/><br />Level Prices:<br />";
		while ($row2 = mysql_fetch_assoc($result2)) {
			$tabla.="<form method='post' action='schoolAbility.php'>Level ".$row2['level'].": <input type='text' size=3 name='priceGold' value='".$row2['priceGold']."'/>Gold, <input type='text' size=3 name='priceDiamond' value='".$row2['priceDiamond']."'/>Diamonds   Update: ";
			$tabla.="<input type='submit' name='levelSchoolAbilities' value='".$row2['pk_levelSchoolAbilities']."' /></form>";
		}
	}
	echo $tabla;
	mysql_free_result($result);
	mysql_free_result($result2);
}

function obtenerSchoolsWeb(){
	$result=ejecutarQuery("select * from schoolAbility");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		//$linkUsuarios="<a href='juego_user.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkLogros="<a href='logros.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkItems="<a href='items.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$tabla.="<tr><td>".$row['pk_schoolAbility']."</td><td>".$row['name']."</td><td><a href='schoolAbility.php?idSchool=".$row['pk_schoolAbility']."'>Modify</a></td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}

function actualizarPilotWeb($idPilot, $active, $name, $description, $priceGold, $priceDiamond){
	$result=ejecutarQuery("update pilot set name='$name', active=$active, description='$description', priceGold=$priceGold, priceDiamond=$priceDiamond where pk_pilot=$idPilot");
}

function obtenerPilotWeb($id){
	$result=ejecutarQuery("select * from pilot where pk_pilot=$id");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.=
		"Active        : <input type='text' name='active' value='".$row['active']."'/><br />".
		"Name          : <input type='text' name='name' value='".$row['name']."'/><br />".
		"Description   : <input type='text' name='description' size=100 MAXLENGTH=200 value='".$row['description']."'/><br />".
		"Price gold    : <input type='text' name='priceGold' value='".$row['priceGold']."'/><br />".
		"Price diamond : <input type='text' name='priceDiamond' value='".$row['priceDiamond']."'/><br />";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerPilotsWeb(){
	$result=ejecutarQuery("select * from pilot");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		//$linkUsuarios="<a href='juego_user.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkLogros="<a href='logros.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkItems="<a href='items.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$tabla.="<tr><td>".$row['pk_pilot']."</td><td>".$row['active']."</td><td>".$row['name']."</td><td>".$row['description']."</td><td>".$row['priceGold']."</td><td>".$row['priceDiamond']."</td><td><a href='pilots.php?idPilot=".$row['pk_pilot']."'>Modify</a></td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}

function actualizarCardWeb($idCard, $active, $name, $description, $rarity){
	$result=ejecutarQuery("update card set name='$name', active=$active, description='$description', rarity=$rarity where pk_card=$idCard");
}

function obtenerCardWeb($id){
	$result=ejecutarQuery("select * from card where pk_card=$id");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.=
		"Active        : <input type='text' name='active' value='".$row['active']."'/><br />".
		"Name          : <input type='text' name='name' value='".$row['name']."'/><br />".
		"Description   : <input type='text' name='description' value='".$row['description']."'/><br />".
		"Rarity        : <input type='text' name='rarity' value='".$row['rarity']."'/><br />";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerCardsWeb(){
	$result=ejecutarQuery("select * from card");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		//$linkUsuarios="<a href='juego_user.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkLogros="<a href='logros.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkItems="<a href='items.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$tabla.="<tr><td>".$row['pk_card']."</td><td>".$row['active']."</td><td>".$row['name']."</td><td>".$row['description']."</td><td>".$row['rarity']."</td><td><a href='cards.php?idCard=".$row['pk_card']."'>Modify</a></td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}

function actualizarBlimpWeb($idBlimp, $active, $name, $priceGold, $priceDiamond, $att1, $att2, $att3, $att4, $att5, $att6, $att7, $att8){
	$result=ejecutarQuery("update blimp set name='$name', active=$active, priceGold=$priceGold, priceDiamond=$priceDiamond, att1=$att1, att2=$att2, att3=$att3, att4=$att4, att5=$att5, att6=$att6, att7=$att7, att8=$att8 where pk_blimp=$idBlimp");
}

function obtenerBlimpWeb($id){
	$result=ejecutarQuery("select * from blimp where pk_blimp=$id");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.=
		"Active        : <input type='text' name='active' value='".$row['active']."'/><br />".
		"Name          : <input type='text' name='name' value='".$row['name']."'/><br />".
		"Price gold    : <input type='text' name='priceGold' value='".$row['priceGold']."'/><br />".
		"Price diamonds: <input type='text' name='priceDiamond' value='".$row['priceDiamond']."'/><br />".
		"Force x       : <input type='text' name='att1' value='".$row['att1']."'/><br />".
		"Force y       : <input type='text' name='att2' value='".$row['att2']."'/><br />".
		"Life          : <input type='text' name='att3' value='".$row['att3']."'/><br />".
		"DMG modifier  : <input type='text' name='att4' value='".$row['att4']."'/><br />".
		"Altitude      : <input type='text' name='att5' value='".$row['att5']."'/><br />".
		"Air           : <input type='text' name='att6' value='".$row['att6']."'/><br />".
		"Mass          : <input type='text' name='att7' value='".$row['att7']."'/><br />".
		"Drag          : <input type='text' name='att8' value='".$row['att8']."'/><br />";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerBlimpsWeb(){
	$result=ejecutarQuery("select * from blimp");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		//$linkUsuarios="<a href='juego_user.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkLogros="<a href='logros.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkItems="<a href='items.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$tabla.="<tr><td>".$row['pk_blimp']."</td><td>".$row['active']."</td><td>".$row['name']."</td><td>".$row['priceGold']."</td><td>".$row['priceDiamond']."</td><td>".$row['att1']."</td><td>".$row['att2']."</td><td>".$row['att3']."</td><td>".$row['att4']."</td><td>".$row['att5']."</td><td>".$row['att6']."</td><td>".$row['att7']."</td><td>".$row['att8']."</td><td><a href='blimps.php?idBlimp=".$row['pk_blimp']."'>Modify</a></td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}

function obtenerUsersWeb(){
	$result=ejecutarQuery("select * from user");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		//$linkUsuarios="<a href='juego_user.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkLogros="<a href='logros.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		//$linkItems="<a href='items.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$tabla.="<tr><td>".$row['pk_user']."</td><td>".$row['username']."</td><td>".$row['email']."</td><td>".$row['gold']."</td><td>".$row['diamond']."</td><td>".$row['totalGold']."</td><td>".$row['totalDiamond']."</td><td>".$row['registerDate']."</td><td>".$row['reference']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}*/
/*
function obtenerJuegos(){
	$result=ejecutarQuery("select * from nemoris_juego");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$linkUsuarios="<a href='juego_user.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$linkLogros="<a href='logros.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$linkItems="<a href='items.php?idJuego=".$row['pk_juego']."'>Ver</a>";
		$tabla.="<tr><td>".$row['pk_juego']."</td><td>".$row['nombre']."</td><td>".$linkUsuarios."</td><td>".$linkLogros."</td><td>".$linkItems."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}

function agregarJuego($nombre){
	$result=ejecutarQuery("insert into nemoris_juego(nombre) values('".$nombre."')");
}

function obtenerJuegoUsuario($idJuego){
	$result=ejecutarQuery("SELECT ju.pk_juego_user, u.username as nombreUser, j.nombre, ju.puntaje FROM nemoris_juego_user ju, jos_comprofiler c, nemoris_juego j, jos_users u WHERE u.id=c.user_id and ju.fk_user=c.user_id and ju.fk_juego=j.pk_juego and ju.fk_juego=".$idJuego);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['pk_juego_user']."</td><td>".$row['nombre']."</td><td>".$row['nombreUser']."</td><td>".$row['puntaje']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
}

function obtenerLogs($idJuego){
	$condicion="";
	if($idJuego!=null) $condicion=" and j.pk_juego=".$idJuego;

	$result=ejecutarQuery("SELECT l.pk_log, u.username as nombreUser, j.nombre, l.mensaje, l.fecha FROM nemoris_logs l, jos_comprofiler c, nemoris_juego j, jos_users u WHERE u.id=c.user_id and l.fk_user=c.user_id and l.fk_juego=j.pk_juego".$condicion." order by l.fecha desc");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['pk_log']."</td><td>".$row['fecha']."</td><td>".$row['nombre']."</td><td>".$row['nombreUser']."</td><td>".$row['mensaje']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}
function obtenerItems($idJuego){
	$condicion="";
	$result=ejecutarQuery("SELECT l.pk_item, j.nombre as nombreJuego, l.nombre, l.precio FROM nemoris_item l, nemoris_juego j WHERE l.fk_juego=j.pk_juego and j.pk_juego=".$idJuego);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['pk_item']."</td><td>".$row['nombreJuego']."</td><td>".$row['nombre']."</td><td>".$row['precio']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}
function obtenerPuntos($idJuego, $idUsuario){
	$result=ejecutarQuery("SELECT dineroJuego FROM nemoris_juego_user where fk_user=".$idUsuario." and fk_juego=".$idJuego);
	while ($row = mysql_fetch_assoc($result)) {
		$puntos=$row['dineroJuego'];
	}
	if($puntos=="") $puntos="0";
	echo $puntos;
	mysql_free_result($result);
	//mysql_close($link);
}
function obtenerLogros($idJuego){
	$condicion="";
	$result=ejecutarQuery("SELECT l.pk_logro, j.nombre as nombreJuego, l.nombre FROM nemoris_logro l, nemoris_juego j WHERE l.fk_juego=j.pk_juego and j.pk_juego=".$idJuego);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['pk_logro']."</td><td>".$row['nombreJuego']."</td><td>".$row['nombre']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}
function obtenerItemsUsuario($idJuego){
	$condicion="";
	$result=ejecutarQuery("SELECT ls.pk_items, l.nombre, u.username as nombreUser, ls.fecha, ls.dineroPagado FROM nemoris_item l, nemoris_items ls, nemoris_juego j, jos_comprofiler c, jos_users u WHERE u.id=c.user_id and l.fk_juego=j.pk_juego AND l.pk_item=ls.fk_item AND ls.fk_user=c.user_id and j.pk_juego=".$idJuego);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['pk_items']."</td><td>".$row['nombre']."</td><td>".$row['dineroPagado']."</td><td>".$row['nombreUser']."</td><td>".$row['fecha']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}
function obtenerLogrosUsuario($idJuego){
	$condicion="";
	$result=ejecutarQuery("SELECT ls.pk_logros, l.nombre, u.username as nombreUser, ls.fecha FROM nemoris_logro l, nemoris_logros ls, nemoris_juego j, jos_comprofiler c, jos_users u WHERE u.id=c.user_id and l.fk_juego=j.pk_juego AND l.pk_logro=ls.fk_logro AND ls.fk_user=c.user_id and j.pk_juego=".$idJuego);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.="<tr><td>".$row['pk_logros']."</td><td>".$row['nombre']."</td><td>".$row['nombreUser']."</td><td>".$row['fecha']."</td></tr>";
	}
	echo $tabla;
	mysql_free_result($result);
	//mysql_close($link);
}
function agregarItem($idJuego, $item, $precio){
	$result=ejecutarQuery("insert into nemoris_item(nombre, fk_juego, precio) values('".$item."',".$idJuego.",".$precio.")");
}
function agregarlogro($idJuego, $logro){
	$result=ejecutarQuery("insert into nemoris_logro(nombre, fk_juego) values('".$logro."',".$idJuego.")");
}
function obtenerPuntajeJugadores($idJuego){
	$result=ejecutarQuery("SELECT u.username as nombreUser, ju.puntaje FROM nemoris_juego_user ju, jos_comprofiler c, jos_users u WHERE u.id=c.user_id and ju.fk_user=c.user_id and ju.fk_juego=".$idJuego." order by ju.puntaje desc");
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.=$row['nombreUser']."|".$row['puntaje']."|";
	}
	$tabla.="";
	echo $tabla;
	mysql_free_result($result);
}
function obtenerPuntajeNivelExp($idUsuario, $idJuego){
	if($idUsuario=="0"){
		$punt="0|1|0";
	}
	else{
		$result=ejecutarQuery("SELECT ju.puntaje, c.cb_nivel, c.cb_experience FROM nemoris_juego_user ju, jos_comprofiler c, jos_users u WHERE u.id=c.user_id and ju.fk_user=c.user_id and ju.fk_juego=".$idJuego." and ju.fk_user=".$idUsuario);
		$punt="";
		while ($row = mysql_fetch_assoc($result)) {
			$punt=$row['puntaje']."|".$row['cb_nivel']."|".$row['cb_experience'];
		}
		if($punt==""){
			guardarPuntaje($idJuego, $idUsuario, 0);
			$result2=ejecutarQuery("SELECT ju.puntaje, c.cb_nivel, c.cb_experience FROM nemoris_juego_user ju, jos_comprofiler c, jos_users u WHERE u.id=c.user_id and ju.fk_user=c.user_id and ju.fk_juego=".$idJuego." and ju.fk_user=".$idUsuario);
			$punt="";
			while ($row2 = mysql_fetch_assoc($result2)) {
					$punt=$row2['puntaje']."|".$row2['cb_nivel']."|".$row2['cb_experience'];
			}
			mysql_free_result($result2);
		}
		mysql_free_result($result);
	}
	return $punt;
}
function obtenerGold($idUsuario){
	if($idUsuario=="0"||$idUsuario=="-1"){
		$punt="0";
	}
	else{
		$result=ejecutarQuery("SELECT c.cb_nemorisgold FROM jos_comprofiler c WHERE c.id=".$idUsuario);
		$punt="";
		while ($row = mysql_fetch_assoc($result)) {
			$punt=$row['cb_nemorisgold'];
		}
		mysql_free_result($result);
	}
	return $punt;
}
function actualizarPuntos($puntos, $idUsuario, $idJuego){
	$result=ejecutarQuery("select pk_juego_user, dineroJuego from nemoris_juego_user where fk_juego=".$idJuego." and fk_user=".$idUsuario." limit 1;");
	while ($row = mysql_fetch_assoc($result)) {
		$idJuegoUser=$row['pk_juego_user'];
	}
	if($idJuegoUser!=null)
		$result=ejecutarQuery("update nemoris_juego_user set dineroJuego=".$puntos." where pk_juego_user=".$idJuegoUser);
	else
		$result=ejecutarQuery("insert into nemoris_juego_user(fk_user, fk_juego, puntaje, dineroJuego) values(".$idUsuario.",".$idJuego.",0,".$puntos.")");
}
function comprarPuntos($puntos, $idUsuario, $precio, $idJuego){
	$result2=ejecutarQuery("SELECT j.cb_nemorisgold FROM jos_comprofiler j  WHERE j.id=".$idUsuario.";");
		while ($row = mysql_fetch_assoc($result2)) {
			$gold=$row['cb_nemorisgold'];
			$saldo=$gold-$precio;
			if($saldo>=0){
				$result3=ejecutarQuery("update jos_comprofiler set cb_nemorisgold=cb_nemorisgold-".$precio." where id=".$idUsuario);
				$result=ejecutarQuery("select pk_juego_user, dineroJuego from nemoris_juego_user where fk_juego=".$idJuego." and fk_user=".$idUsuario." limit 1;");
				while ($row = mysql_fetch_assoc($result)) {
					$idJuegoUser=$row['pk_juego_user'];
				}
				if($idJuegoUser!=null)
					$result=ejecutarQuery("update nemoris_juego_user set dineroJuego=dineroJuego+".$puntos." where pk_juego_user=".$idJuegoUser);
				else
					$result=ejecutarQuery("insert into nemoris_juego_user(fk_user, fk_juego, puntaje, dineroJuego) values(".$idUsuario.",".$idJuego.",0,".$puntos.")");
				echo $saldo;
			}
			else{
				echo "-1";
			}
		}
}
function comprarItem($idItem, $idUsuario, $precio){
	$comprado=false;
	$result=ejecutarQuery("SELECT * FROM nemoris_items  WHERE fk_item=".$idItem." and fk_user=".$idUsuario.";");
	while ($row = mysql_fetch_assoc($result)) {
		echo "-2";
		$comprado=true;
	}
	mysql_free_result($result);
	if(!$comprado){
		$result=ejecutarQuery("SELECT j.cb_nemorisgold, i.precio FROM jos_comprofiler j, nemoris_item i  WHERE i.pk_item=".$idItem." and j.id=".$idUsuario.";");
		while ($row = mysql_fetch_assoc($result)) {
			$gold=$row['cb_nemorisgold'];
			$prec=$row['precio'];
			$saldo=$gold-$prec;
			if($saldo>=0&&$precio-$prec==0){
				$result2=ejecutarQuery("insert into nemoris_items(fk_user, fk_item, dineroPagado, fecha) values(".$idUsuario.",".$idItem.",".$precio.",NOW())");
				$result2=ejecutarQuery("update jos_comprofiler set cb_nemorisgold=cb_nemorisgold-".$precio." where id=".$idUsuario);
				echo $saldo;
			}
			else{
				echo "-1";
			}
		}
		mysql_free_result($result);
	}
	
}
function guardarPuntaje($idJuego, $idUsuario, $puntaje){
	$result=ejecutarQuery("select pk_juego_user, puntaje from nemoris_juego_user where fk_juego=".$idJuego." and fk_user=".$idUsuario." limit 1;");
	while ($row = mysql_fetch_assoc($result)) {
		$idJuegoUser=$row['pk_juego_user'];
	}
	if($idJuegoUser!=null)
		$result=ejecutarQuery("update nemoris_juego_user set puntaje=puntaje+".$puntaje." where pk_juego_user=".$idJuegoUser);
	else
		$result=ejecutarQuery("insert into nemoris_juego_user(fk_user, fk_juego, puntaje) values(".$idUsuario.",".$idJuego.",".$puntaje.")");
}
function obtenerItemsJuego($idJuego){
	$result=ejecutarQuery("SELECT l.pk_item, l.precio FROM nemoris_item l, nemoris_juego j WHERE l.fk_juego=j.pk_juego and j.pk_juego=".$idJuego);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.=$row['pk_item']."|".$row['precio']."|";
	}
	echo $tabla;
	mysql_free_result($result);
}
function obtenerItemsUsuarioJuego($idJuego, $idUsuario){
	$result=ejecutarQuery("SELECT l.pk_item, l.precio FROM nemoris_item l, nemoris_items ls, nemoris_juego j WHERE l.fk_juego=j.pk_juego AND l.pk_item=ls.fk_item AND ls.fk_user=".$idUsuario." and j.pk_juego=".$idJuego);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.=$row['pk_item']."|";
	}
	echo $tabla;
	mysql_free_result($result);
}
function obtenerLogrosUsuarioJuego($idJuego, $idUsuario){
	$result=ejecutarQuery("SELECT l.pk_logro FROM nemoris_logro l, nemoris_logros ls, nemoris_juego j WHERE l.fk_juego=j.pk_juego AND l.pk_logro=ls.fk_logro AND ls.fk_user=".$idUsuario." and j.pk_juego=".$idJuego);
	$tabla="";
	while ($row = mysql_fetch_assoc($result)) {
		$tabla.=$row['pk_logro']."|";
	}
	echo $tabla;
	mysql_free_result($result);
}
function guardarItem($idUsuario, $idItem){
	$result=ejecutarQuery("SELECT ls.pk_items FROM nemoris_items ls WHERE ls.fk_item=".$idItem." AND ls.fk_user=".$idUsuario);
	
	if (mysql_num_rows($result) == 0) {
		mysql_free_result($result);
		$result2=ejecutarQuery("insert into nemoris_items(fk_item, fk_user, fecha) values (".$idItem.",".$idUsuario.",NOW());");
		mysql_free_result($result2);
	}
}
function guardarLogro($idUsuario, $idLogro){
	$result=ejecutarQuery("SELECT ls.pk_logros FROM nemoris_logros ls WHERE ls.fk_logro=".$idLogro." AND ls.fk_user=".$idUsuario);
	
	if (mysql_num_rows($result) == 0) {
		mysql_free_result($result);
		$result2=ejecutarQuery("insert into nemoris_logros(fk_logro, fk_user, fecha) values (".$idLogro.",".$idUsuario.",NOW());");
		mysql_free_result($result2);
	}
}*/
/*
function agregarGold($idUsuario, $cantidad, $total){
	$result=ejecutarQuery("update jos_comprofiler set cb_nemorisgold=cb_nemorisgold+".$cantidad.", cb_nemorisgoldtotal=".$total." where id=".$idUsuario);
	//mysql_free_result($result);
}
function obtenerGoldTotal($idUsuario){
	$result=ejecutarQuery("select cb_nemorisgoldtotal from jos_comprofiler where id=".$idUsuario);
	$total=0;
	while ($row = mysql_fetch_assoc($result)) {
		$total=$row['cb_nemorisgoldtotal'];	
	}
	mysql_free_result($result);
	return $total;
	//mysql_close($link);
}
function registrarUsuarioJugando($idJuego, $idUsuario){
	$result=ejecutarQuery("update jos_comprofiler set cb_jugadas=cb_jugadas+1 where user_id=".$idUsuario);
	mysql_free_result($result);
}
function registrarJugando($idJuego, $reg){
	if($reg==0) $result=ejecutarQuery("update nemoris_juego set jugado=jugado+1 where pk_juego=".$idJuego);
	else $result2=ejecutarQuery("update nemoris_juego set jugadoRegistrado=jugadoRegistrado+1 where pk_juego=".$idJuego);
	
}
function registrarExperiencia($idJuego, $idUsuario, $exp){
	$result4=ejecutarQuery("update jos_comprofiler set cb_experience=0, cb_nivel=1, cb_jugadas=1 where (cb_experience is null or cb_nivel is null or cb_jugadas is null) and user_id=".$idUsuario);
	$result=ejecutarQuery("update jos_comprofiler set cb_experience=cb_experience+".$exp." where user_id=".$idUsuario);
	$result2=ejecutarQuery("SELECT cb_experience, cb_nivel FROM jos_comprofiler WHERE user_id=".$idUsuario);
	while ($row = mysql_fetch_assoc($result2)) {
		$expTotal=$row['cb_experience'];
		$nivelTotal=$row['cb_nivel'];
		$nivelInicial=$nivelTotal;
		$expSubir=0;
		while($expTotal>$expSubir){
			$nivelTotal++;
			$expSubir=7*log($nivelTotal*0.4,2.7)+pow($nivelTotal,2.3)+10;
		}
		if($nivelInicial==$nivelTotal-1){
			echo "-1";
		}
		else{ 
			$result3=ejecutarQuery("update jos_comprofiler set cb_nivel=".($nivelTotal-1)." where user_id=".$idUsuario);
			echo $nivelTotal-1;
		}
	}
	mysql_free_result($result2);
}*/
?>