<?php
/*
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
*/
date_default_timezone_set('UTC');

// Verify that all of the parameters have been set.
if(isset($_POST['username']) && isset($_POST['password']) && isset($_POST['cheese']) && isset($_POST['parms']))
{
    $api = new cheesey_api($_POST['username'], $_POST['password'], $_POST['cheese'], $_POST['parms']);
}

else
{
    $datalogger = new data_logger();
}

/**
 * Self contained Api object with proper access modifyers.
 */
class cheesey_api
{
    private $connection = null;
    private $user_account = null;
    private $user_password = null;
    private $server_response = ['key' => ''];

    function __construct($username, $password, $cheese, $parmesan)
    {
        if(isset($username) && isset($password) && isset($cheese) && isset($parmesan))
        {
            $DATABASE_HOST = 'localhost';
            $DATABASE_USER = 'admin';
            $DATABASE_PASS = 'JeffStar';
            $DATABASE_NAME = 'USER_INFO_DB';

            $this->user_account = $this->stripAllSymbols($username);
            $this->user_password = $this->stripSomeSymbols($password);
            $this->connection = mysqli_connect($DATABASE_HOST, $DATABASE_USER, $DATABASE_PASS, $DATABASE_NAME);

            if (mysqli_connect_errno())
            {
                return 'Failed to connect to MySQL: ' . mysqli_connect_error();
            }

            else
            {
                if ($this->login() && $this->logIp())
                {
                    switch ($cheese)
                    {
                        case 'login':
                            echo 1;
                            break;

                        case 'add_user': // Adds a user to the cheat api
                            $eggnoodle = json_decode($parmesan, true);
                            echo $this->addUser($eggnoodle);
                            break;

                        case 'delete_user': // Delete a user from the cheat api
                            $eggnoodle = json_decode($parmesan, true);
                            echo $this->removeUser($eggnoodle);
                            break;

                        case 'time_check':
                            $eggnoodle = json_decode($parmesan, true);
                            echo $this->checkTime($eggnoodle);
                            break;

                        case 'add_key':
                            $eggnoodle = json_decode($parmesan, true);
                            echo $this->addKey($eggnoodle);
                            break;

                        case 'redeem_key':
                            $eggnoodle = json_decode($parmesan, true);
                            echo $this->redeemKey($eggnoodle);
                            break;

                        default:
                            echo 0;
                            break;
                    }
                }

                else
                {
                    echo 0;
                }
            }
        }
    }

    /**
     * email, username, password, lifetime, hardwareid, admin
     * @param $parmesan
     * @return bool
     */
    private function addUser($parmesan)
    {
        $email = $this->stripSomeSymbols($parmesan['email']);
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
                    return 1;
                }
            }
        }

        return 0;
    }

    /**
     * email, username, password
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
                $remove_user_query->bind_param('sss', $email, $username, $password);
                $remove_user_query->execute();
                $remove_user_query->store_result();
    
                if($remove_user_query->affected_rows > 0)
                {
                    return 1;
                }
            }
        }

        return 0;
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
                return 1;
            }
        }

        return 0;
    }

    /**
     * User admin check.
     * @return bool
     */
    private function isAdmin()
    {
        if($this->login())
        {
            if($login_query = $this->connection->prepare('SELECT ADMIN FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
            {
                $login_query->bind_param('ss', $this->user_account, $this->user_password);
                $login_query->execute();
                $login_query->store_result();
                
                if($login_query->num_rows > 0)
                {
                    $login_query->bind_result($admin);
                    $login_query->fetch();
    
                    return $admin;
                }
            }
        }

        return 0;
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
                                $r = rand(0, 14);
            
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

        return 0;
    }


    private function redeemKey($parmesan)
    {
        //$key = $this->stripSomeSymbols($parmesan['key']);

        return $this->responseKey();
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
                    $time_left = (strtotime($auth_end_date) - strtotime(date("d-m-Y h:i:s A")));

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
                    $ip_query->bind_param('is', $id, data_logger::getIPAddress());
           
                    if($ip_query->execute())
                    {
                        return 1;
                    }
                }
            }
        }

        return 0;
    }

    public function responseKey()
    {
        $carray = [ 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'X', 'Y', 'Z' ];
        $dayinyear = date('z') + 1;
        $year = date("Y");
        $a = ($year + $dayinyear) | ($year * $dayinyear) + ($year / $dayinyear);
        $x = ($year + $dayinyear) ^ ($year | $dayinyear) + ($year ^ $dayinyear);
        $numberstring = (string)abs($a * $x * (1 - $x));
        $retVal = "@";

        $chars = str_split($numberstring);
        foreach($chars as $char)
        {
            if(is_numeric($char))
            {
                $retVal .= $carray[intval($char)];
            }
        }

        return $retVal;
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
                $log_anon_ip_query->bind_param('s', data_logger::getIPAddress());
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
