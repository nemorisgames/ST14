<?php
//NO SE PUEDE ENVIAR UN CORREO DE CONFIRMACION POR ESTA VIA
//SIN UN CORREO REAL DEL USUARIO ESTO NO TIENE SENTIDO
//SE CONTINUA MOSTRANDO UN MENSAJE DE QUE ES MUY NECESARIO INGRESAR UN CORREO REAL
//include 'funciones.php';

//ERRORES
//-1: USUARIO YA EXISTE
//-2: ERROR EN QUERY

$username_quitar=$_POST['username'];
$password_quitar=$_POST['password'];
$nombreReal=$_POST['nombre'];
$sexo=$_POST['sexo'];
$mail=$_POST['mail'];
$estado=$_POST['estado'];
$direccion=$_POST['direccion'];
$edad=$_POST['edad'];


//$imagenReal = mysql_real_escape_string($imagen);




include("funciones.php");
   $dbaddress='localhost'; $dbuser='nemorisg_LHD'; $dbpass='Simulador1'; $dbname='nemorisg_simuladorLHD';
   
$dbcnx = mysql_connect($dbaddress,$dbuser,$dbpass)
or die("Could not connect: " . mysql_error());
mysql_select_db($dbname, $dbcnx) or die ('Unable to select the database: ' . mysql_error());


$query = mysql_query("select IdAdministrador from Administrador where IdAdministrador = \"" . $username_quitar . "\"")
or die("-2");


$result = mysql_fetch_array($query);
$respuesta=$result[0];

if($respuesta!=""){ 
	echo "ya creado"; 
	return; 
}

/*if($idFacebook == ""){
$query = mysql_query("select email from Usuario where email = \"" . $email_quitar . "\"")
or die("-2");
}

$result = mysql_fetch_array($query);
$respuesta=$result[0];

if($respuesta!=""){ echo "-3"; return; }*/

//a�adimos a la tabla user
else{
	$query = mysql_query("insert into Administrador(IdAdministrador, Pass, Nombre, Sexo, Mail) values (\"" . $username_quitar . "\",\"" . md5($password_quitar). "\" ,\"" . $nombreReal . "\" ,\"" . $sexo . "\",\"".$mail."\")") or die("-3");
	
	
	//rescatamos el id de usuario
	$query = mysql_query("select IdAdministrador from Administrador where IdAdministrador = \"" . $username_quitar . "\"")
	or die("-2");
	$result = mysql_fetch_array($query);
	$id_user=$result[0];
	
	echo $id_user;
	}

//a�adimos el primer piloto
//$query = mysql_query("insert into pilot_user(fk_user, fk_pilot, purchaseDate, exp, level) values(".$id_user.", 1, NOW(), 0, 1)") or die("-2");

/*$result2=mysql_query("SELECT pk_pilot_schoolAbility FROM pilot_schoolAbility WHERE fk_pilot=1;") or die("-2");
$result3=mysql_query("SELECT pk_pilot_user FROM pilot_user WHERE fk_user=$id_user and fk_pilot=1;") or die("-2");
$row2 = mysql_fetch_assoc($result3);
while ($row = mysql_fetch_assoc($result2)) {
	$result3=mysql_query("insert into pilot_level_schoolAbility(fk_pilot_schoolAbility, fk_pilot_user, currentLevel) values(".$row['pk_pilot_schoolAbility'].", ".$row2['pk_pilot_user'].", 0)") or die("-2");
}
*/
//a�adimos el primer blimp
//$query = mysql_query("insert into blimp_user(fk_user, fk_blimp, purchaseDate) values(".$id_user.", 2, NOW())") or die("-2");

//rescatamos el id de usuario
//$query = mysql_query("select pk_user from user where username = \"" . $username_quitar . "\"")
//or die("-2");
//$result = mysql_fetch_array($query);
//$id_user=$result[0];

//a�adimos a la tabla jos_comprofiler
//$query = mysql_query("insert into jos_comprofiler(id, user_id, firstname) values (".$id_user.", ".$id_user.",\"" . $nombre_quitar . "\")") or die("-2");

//$query = mysql_query("insert into jos_core_acl_aro(section_value, value, name) values ('users', ".$id_user.",\"" . $nombre_quitar . "\")") or die("-2");
//rescatamos el id de usuario
//$query = mysql_query("select id from jos_core_acl_aro where value = " . $id_user . "") or die("-2");
//$result = mysql_fetch_array($query);
//$id_aro=$result[0];

//$query = mysql_query("insert into jos_core_acl_groups_aro_map(group_id, aro_id) values (18, ".$id_aro.")") or die("-2");

?>