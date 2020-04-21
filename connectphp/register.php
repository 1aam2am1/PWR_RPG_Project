<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unity";


//variables submited by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"]; 

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username FROM account where username = '" . $loginUser ."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
echo "User is already exist";
} 
else
	{
 $sql2 = "INSERT INTO account (username, password) VALUES ('" . $loginUser . "', '" . $loginPass ."')";
	if($conn->query($sql2) == TRUE)
	{
		echo "Account Created";
	
	}
	else 
	{
		echo "Error: " . $sql2 . "<br>" . $conn->error;
	}
	}
	
	
$conn->close();
?>