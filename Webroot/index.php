<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
date_default_timezone_set('UTC');

// Get the encryption key
if(isset($_POST['cheese'])) {
    if($_POST['cheese'] == "90kGPILHd22/yQ3bctAPwxzEPq+BEA4og3Wqh+hSRFQ=")
    {
        $dayinyear = date('z') + 1;
        $year = date("Y");
        echo base64_encode($dayinyear + $year);
    }
}

else if(isset($_POST['bluecheese']))
{
    $api = new cheesey_api($_POST['bluecheese']);
}

else
{
    $datalogger = new data_logger();
    http_response_code(404);
}

/**
 * Self contained Api object with proper access modifyers.
 */
class cheesey_api
{
    private $iv = null;
    private $dayinyear = 0;
    private $year = 0;
    private $eKey = "";
    private $dKey = "";

    private $connection = null;
    private $user_account = null;
    private $user_password = null;

    function __construct($bluecheese)
    {
        $this->iv = chr(0x0) . chr(0xf) . chr(0x0) . chr(0xf) . chr(0x0) . chr(0xf) . chr(0x0) . chr(0xf) . chr(0x0) . chr(0xf) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0xe) . chr(0x0);
        $this->dayinyear = date('z') + 1;
        $this->year = date("Y");
        $this->dKey = base64_encode($this->dayinyear + $this->year);
        $this->eKey = base64_encode($this->dayinyear - $this->year);
        $decryptedInput = json_decode($this->decryptString($bluecheese));

        if(isset($decryptedInput->username) && isset($decryptedInput->password) && isset($decryptedInput->cheese) && isset($decryptedInput->parms))
        {
            $DATABASE_HOST = 'localhost';
            $DATABASE_USER = 'admin';
            $DATABASE_PASS = 'JeffStar';
            $DATABASE_NAME = 'USER_INFO_DB';

            $this->user_account = $this->stripAllSymbols($decryptedInput->username);
            $this->user_password = $this->stripSomeSymbols($decryptedInput->password);
            $this->connection = mysqli_connect($DATABASE_HOST, $DATABASE_USER, $DATABASE_PASS, $DATABASE_NAME);

            if (mysqli_connect_errno())
            {
                return 'Failed to connect to MySQL: ' . mysqli_connect_error();
            }

            else
            {
                if ($this->login() && $this->logIp())
                {
                    $parmesan = $decryptedInput->parms;

                    switch ($decryptedInput->cheese)
                    {
                        case 'get_dkey':
                            echo json_encode(['dkey' => $this->eKey, 'heartrate' => 13, 'heartrhythm' => 500], true);
                            break;

                        case 'login':
                            $json = json_encode(['loggedin' => true], true);
                            echo $this->encryptString($json);
                            break;

                        case 'time_check':
                            $eggnoodle = json_decode($parmesan, true);
                            $json = json_encode(['timeleft' => $this->checkTime($eggnoodle)], true);
                            echo $this->encryptString($json);
                            break;

                        case 'redeem_key':
                            $eggnoodle = json_decode($parmesan, true);
                            $json = json_encode(['keyres' => $this->redeemKey($eggnoodle)], true);
                            echo $this->encryptString($json);
                            break;

                        // Admin commands
                        case 'add_user': // Adds a user to the cheat api
                            $eggnoodle = json_decode($parmesan, true);
                            $json = json_encode(['addres' => $this->addUser($eggnoodle)], true);
                            echo $this->encryptString($json);
                            break;

                        case 'delete_user': // Delete a user from the cheat api
                            $eggnoodle = json_decode($parmesan, true);
                            $json = json_encode(['deleteres' => $this->removeUser($eggnoodle)], true);
                            echo $this->encryptString($json);
                            break;

                        case 'add_key':
                            $eggnoodle = json_decode($parmesan, true);
                            $key = $this->addKey($eggnoodle);
                            $b = !($key == false);
                            $json = json_encode(['key' => $key], true);
                            echo $this->encryptString($json);
                            break;

                        default:
                            // Im a teapot, not a coffee maker.
                            http_response_code(418);
                            break;
                    }
                }

                else
                {
                    http_response_code(404);
                }
            }
        }

        else
        {
            http_response_code(404);
            $datalogger = new data_logger();
        }
    }

    /**
     * email, username, password, lifetime, hardwareid, admin
     * @param $parmesan
     * @return bool
     */
    private function addUser($parmesan)
    {
        $email = $parmesan['email'];
        $username = $this->stripAllSymbols($parmesan['username']);
        $password = $this->stripSomeSymbols($parmesan['password']);
        $admin = $this->stripAllSymbols($parmesan['admin']);
        $ip = data_logger::getIPAddress();

        if($this->isAdmin())
        {
            if ($add_user_query = $this->connection->prepare('call addUser(?, ?, ?, ?, ?)'))
            {
                $add_user_query->bind_param('ssssi', $email, $username, $password, $ip, $admin);
                $add_user_query->execute();
                $add_user_query->store_result();

                if($add_user_query->affected_rows > 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /**
     * username
     * @param $parmesan
     * @return bool
     */
    private function removeUser($parmesan)
    {
        $username = $this->stripAllSymbols($parmesan['username']);

        if($this->isAdmin())
        {
            if ($remove_user_query = $this->connection->prepare('DELETE FROM USER WHERE USER_NAME = ?'))
            {
                $remove_user_query->bind_param('s', $username);
                $remove_user_query->execute();
                $remove_user_query->store_result();
    
                if($remove_user_query->affected_rows > 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /**
     * User authentication check.
     * @return bool
     */
    private function login()
    {
        if ($login_query = $this->connection->prepare('SELECT USER_ID FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
        {
            $login_query->bind_param('ss', $this->user_account, $this->user_password);
            $login_query->execute();
            $login_query->store_result();

            if($login_query->num_rows > 0)
            {
                return true;
            }
        }

        return false;
    }

    /**
     * User admin check.
     * @return bool
     */
    private function isAdmin()
    {
        if($this->login())
        {
            if($login_query = $this->connection->prepare('SELECT IS_ADMIN FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
            {
                $login_query->bind_param('ss', $this->user_account, $this->user_password);
                $login_query->execute();
                $login_query->store_result();
                
                if($login_query->num_rows > 0)
                {
                    $login_query->bind_result($admin);
                    $login_query->fetch();
    
                    return boolval($admin);
                }
            }
        }

        return false;
    }

    /**
     * Time Value
     * @param $parmesan
     * @return string|bool
     */
    private function addKey($parmesan)
    {
        if($this->isAdmin())
        {
            if($login_query = $this->connection->prepare('SELECT USER_ID FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
            {
                $login_query->bind_param('ss', $this->user_account, $this->user_password);
                $login_query->execute();
                $login_query->store_result();
    
                if($login_query->num_rows > 0)
                {
                    $login_query->bind_result($id);
                    $login_query->fetch();

                    if(!function_exists("genKey"))
                    {
                        function genKey()
                        {
                            $chars =
                                [
                                    'A',
                                    'B',
                                    'C',
                                    'D',
                                    'E',
                                    'F',
                                    '0',
                                    '1',
                                    '2',
                                    '3',
                                    '4',
                                    '5',
                                    '6',
                                    '7',
                                    '8',
                                    '9'
                                ];
            
                            $key = '';
            
                            for($i = 0; $i <= 20;$i++)
                            {
                                $r = rand(0, 15);
            
                                if($i == 1)
                                {
                                    $key = $chars[$r];
                                }
            
                                else
                                {
                                    $key .= $chars[$r];
                                }
            
                                if(!($i % 5) && $i != 0 && $i != 20)
                                {
                                    $key .= '-';
                                }
                            }
            
                            return $key;
                        }
                    }
            
                    $time_value = $this->stripSomeSymbols($parmesan['time_value']);
                    $key = genKey();
            
                    if($key_exists_query = $this->connection->prepare('select TIME_KEY from TIME_KEYS where TIME_KEY = ?'))
                    {
                        $key_exists_query->bind_param('s', $key);
                        $key_exists_query->execute();
                        $key_exists_query->store_result();
            
                        if($key_exists_query->num_rows == 0)
                        {
                            if ($add_key_query = $this->connection->prepare('call addKey(?, ?, ?)'))
                            {
                                $add_key_query->bind_param('sss', $key, $time_value, $id);
                                $add_key_query->execute();
                                $add_key_query->store_result();
            
                                if($add_key_query->affected_rows > 0)
                                {
                                    return $key;
                                }
                            }
                        }
            
                        else
                        {
                            return $this->addKey($parmesan);
                        }
                    }
                }
            }
        }

        return false;
    }

    /**
     * Key
     * @param $parmesan
     * @return bool
     */
    private function redeemKey($parmesan)
    {
        $key = $parmesan['key'];
        $username = $this->stripAllSymbols($parmesan['username']);

        if($uid_query = $this->connection->prepare('SELECT USER_ID FROM USER WHERE USER_NAME = ?'))
        {
            $uid_query->bind_param('s', $username);
            $uid_query->execute();
            $uid_query->store_result();

            if($uid_query->num_rows <= 0)
                return false;

            $uid_query->bind_result($id);
            $uid_query->fetch();

            if($key_query = $this->connection->prepare('call redeemKey(?, ?)'))
            {
                $key_query->bind_param('si', $key, $id);
                $key_query->execute();
                $key_query->store_result();

                if($key_query->affected_rows == 2)
                    return true;
            }
        }

        return false;
    }

    /**
     * Must pass username, password, ip_addy
     * @param $parm
     * @return bool
     */
    private function checkTime($parmesan)
    {
        $user = $this->stripAllSymbols($parmesan['username']);

        if ($check_time_query = $this->connection->prepare('select AUTH_END_DATE from USER where USER_NAME = ?'))
        {
            $check_time_query->bind_param('s', $user);
            $check_time_query->execute();
            $check_time_query->store_result();
            $check_time_query->bind_result($auth_end_date);

            if($check_time_query->affected_rows > 0)
            {
                // 2022-03-19 23:08:33 SQL date time syntax...
                while($check_time_query->fetch())
                {
                    $time_left = (strtotime($auth_end_date) - strtotime(date("Y-m-d H:i:s")));

                    if($time_left > 0)
                    {
                        return $time_left;
                    }
                }
            }
        }

        return 0;
    }

    /**
     * Just log ip.
     * @param $parm
     * @return bool
     */
    private function logIp()
    {
        if($login_query = $this->connection->prepare('SELECT USER_ID FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
        {
            $login_query->bind_param('ss', $this->user_account, $this->user_password);
            $login_query->execute();
            $login_query->store_result();

            if($login_query->num_rows > 0)
            {
                $login_query->bind_result($id);
                $login_query->fetch();

                if ($ip_query = $this->connection->prepare('call logIP(?, ?)'))
                {
                    $IPAddress = data_logger::getIPAddress();
                    $ip_query->bind_param('is', $id, $IPAddress);
           
                    if($ip_query->execute())
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private function encryptString($plainText)
    {
        $password = substr(hash('sha256', $this->eKey, true), 0, 32);
        return base64_encode(openssl_encrypt($plainText, 'aes-256-cbc', $password, OPENSSL_RAW_DATA, $this->iv));
    }

    private function decryptString($encryptedString)
    {
        $password = substr(hash('sha256', $this->dKey, true), 0, 32);
        return openssl_decrypt(base64_decode($encryptedString), 'aes-256-cbc', $password, OPENSSL_RAW_DATA, $this->iv);
    }

    private function stripAllSymbols($inputStream)
    {
        $outputStreams = preg_replace('/[^0-9a-zA-Z]+/', '', $inputStream);
        return $outputStreams;
    }

    private function stripSomeSymbols($inputStream)
    {
        $outputStreams = preg_replace('/[^0-9a-zA-Z.!@#$%^&*-_]+/', '', $inputStream);
        return $outputStreams;
    }
}

/**
 * Used to log all of the trafic to this page.
 */
class data_logger
{
    function __construct()
    {
        $DATABASE_HOST = 'localhost';
        $DATABASE_USER = 'admin';
        $DATABASE_PASS = 'JeffStar';
        $DATABASE_NAME = 'API_NETWORK_INFO_DB';
        $connection   = null;

        $this->connection = mysqli_connect($DATABASE_HOST, $DATABASE_USER, $DATABASE_PASS, $DATABASE_NAME);

        if (mysqli_connect_errno())
        {
            return 'Failed to connect to MySQL: ' . mysqli_connect_error();
        }

        else
        {
            if($log_anon_ip_query = $this->connection->prepare('call logAnonIp(?)'))
            {
                $IPAddress = data_logger::getIPAddress();
                $log_anon_ip_query->bind_param('s', $IPAddress);
                $log_anon_ip_query->execute();
            }
        }
    }

    /**
     * Pretty self explanitory.
     */
    public static function getIPAddress() {
		$ipaddress = NULL;
		if (getenv('HTTP_CLIENT_IP')){
			$ipaddress = getenv('HTTP_CLIENT_IP');
		}
		else if(getenv('HTTP_X_FORWARDED_FOR')){
			$ipaddress = getenv('HTTP_X_FORWARDED_FOR');
		}
		else if(getenv('HTTP_X_FORWARDED')){
			$ipaddress = getenv('HTTP_X_FORWARDED');
		}
		else if(getenv('HTTP_FORWARDED_FOR')){
			$ipaddress = getenv('HTTP_FORWARDED_FOR');
		}
		else if(getenv('HTTP_FORWARDED')){
			$ipaddress = getenv('HTTP_FORWARDED');
		}
		else if(getenv('REMOTE_ADDR')){
			$ipaddress = getenv('REMOTE_ADDR');
		} else {
			$ipaddress = 'UNKNOWN';
		}
		return $ipaddress;
	}
}
?>
