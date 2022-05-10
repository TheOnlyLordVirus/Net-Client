<?php
//ini_set('display_errors', 1);
//ini_set('display_startup_errors', 1);
//error_reporting(E_ALL);

date_default_timezone_set('UTC');

// Get the encryption key
if(isset($_POST['cheese']))
{
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

        if(isset($decryptedInput->username) && isset($decryptedInput->password) && isset($decryptedInput->cheese) && isset($decryptedInput->noodles) && isset($decryptedInput->parms))
        {
            $DATABASE_HOST = 'localhost';
            $DATABASE_USER = 'admin';
            $DATABASE_PASS = 'JeffStar';
            $DATABASE_NAME = 'USER_INFO_DB';

            $this->user_account = $this->regexRealText($decryptedInput->username);
            $this->user_password = $this->encryptPassword($this->regexPassword($decryptedInput->password));

            $this->connection = mysqli_connect($DATABASE_HOST, $DATABASE_USER, $DATABASE_PASS, $DATABASE_NAME);

            if($this->checkHeaders($decryptedInput) && $this->checkNoodles($decryptedInput))
            {
                if (mysqli_connect_errno())
                {
                    return 'Failed to connect to MySQL: ' . mysqli_connect_error();
                }

                else
                {
                    $login_status = $this->login();

                    // Register new user, return unencrypted response.
                    if ($decryptedInput->cheese == "register_user")
                    {
                        echo json_encode(['addres' => $this->registerUser(json_decode($decryptedInput->parms, true)), "dkey" => $this->eKey], true);
                    }

                    // First login, get decryption key
                    else if($decryptedInput->cheese == "get_dkey") 
                    {
                        if($login_status == "Logged_In" || $login_status == "Logged_In_Without_Time")
                        {
                            if($this->logCommand($decryptedInput->cheese, $decryptedInput->parms) && $this->checkCurrentIp())
                            {
                                echo json_encode(['loggedin' => $login_status, 'dkey' => $this->eKey, 'heartrate' => 13, 'heartrhythm' => 500, "meatball" => intval(hrtime(true)), "gamesjson" => $this->getGameCheats(json_decode($decryptedInput->parms, true)['bitcount'])], true);
                            }

                            else
                            {
                                echo json_encode(['loggedin' => "IP_Mismatch"], true); 
                            }
                        }

                        else
                        {
                            echo json_encode(['loggedin' => $login_status], true);
                        }
                    }

                    else if ($login_status == "Logged_In" || $login_status == "Logged_In_Without_Time")
                    {
                        if($this->logCommand($decryptedInput->cheese, $decryptedInput->parms) && $this->checkCurrentIp())
                        {
                            $parmesan = $decryptedInput->parms;

                            switch ($decryptedInput->cheese)
                            {
                                case 'login':
                                    $json = json_encode(['loggedin' => $login_status, "meatball" => intval(hrtime(true))], true);
                                    echo $this->encryptString($json);
                                    break;

                                case 'change_password':
                                    $json = json_encode(['passchanged' => $this->changePassword($parmesan)], true);
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
                                
                                case 'download_cheat':
                                    if($this->checkTime(['username' => $this->user_account]))
                                    {
                                        $eggnoodle = json_decode($parmesan, true);
                                        $json = json_encode(['file' => $this->downloadCheat($eggnoodle['game'])], true);
                                        echo $this->encryptString($json);
                                    }

                                    else
                                    {
                                        $json = json_encode(['error' => true], true);
                                        echo $this->encryptString($json);
                                    }
                                    break;
                                
                                case 'download_json':
                                    if($this->checkTime(['username' => $this->user_account]))
                                    {
                                        $eggnoodle = json_decode($parmesan, true);
                                        $json = json_encode(['file' => $this->downloadJson($eggnoodle['game'])], true);
                                        echo $this->encryptString($json);
                                    }

                                    else
                                    {
                                        $json = json_encode(['error' => true], true);
                                        echo $this->encryptString($json);
                                    }
                                    break;

                                // Admin commands
                                case 'add_user':
                                    $eggnoodle = json_decode($parmesan, true);
                                    $json = json_encode(['addres' => $this->addUser($eggnoodle)], true);
                                    echo $this->encryptString($json);
                                    break;
                                
                                case 'ban_user':
                                    $eggnoodle = json_decode($parmesan, true);
                                    $json = json_encode(['banres' => $this->removeUser($eggnoodle)], true);
                                    echo $this->encryptString($json);
                                    break;
                                
                                case 'add_key':
                                    $eggnoodle = json_decode($parmesan, true);
                                    $key = $this->addKey($eggnoodle);
                                    $b = !($key == false);
                                    $json = json_encode(['key' => $key], true);
                                    echo $this->encryptString($json);
                                    break;
                                
                                case 'add_key_bulk':
                                    $eggnoodle = json_decode($parmesan, true);
                                    $keyAmount = $eggnoodle['key_amount'];
                                    $keyTokenList = '';
                                
                                    for($i = 0; $i < $keyAmount; $i++)
                                    {
                                        $key = $this->addKey($eggnoodle);
                                        $keyTokenList .= $key . '|';
                                    }

                                    $json = json_encode(['key' => $keyTokenList], true);
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
                            $json = json_encode(['loggedin' => "IP_Mismatch"], true);
                            echo $this->encryptString($json);
                        }
                    }

                    else if($login_status == "User_doesnt_Exist")
                    {
                        $json = json_encode(['loggedin' => $login_status], true);
                        echo $this->encryptString($json);
                    }

                    else if ($login_status == "Password_Failure")
                    {
                        $json = json_encode(['loggedin' => $login_status], true);
                        echo $this->encryptString($json);
                    }
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
     * Check the header responses.
     * @return bool
     */
    private function checkHeaders($decryptedInput)
    {
        $returnMe = true;
        $headers = getallheaders();

        // Random Bullshit challenges.
        if($headers['E357FA3E1796978F'] != "86585B78DAFE862A") 
        {
            $returnMe = false;
        }

        if($headers['57ACFA58FDD45144'] != "46F05E18E29ECD13") 
        {
            $returnMe = false;
        }

        if($headers['57ACFB58FDD452F7'] != "4C6D7290ACC036BF") 
        {
            $returnMe = false;
        }

        if($headers['57ACF858FDD44DDE'] != "731AD80D65542AE4") 
        {
            $returnMe = false;
        }

        if(!($headers['57ACF958FDD44F91'] == "MDAwMDlFM0YxMDcwMDQ3OQ==" || $headers['57ACF958FDD44F91'] == "MDAwMEExMEY3QzI2RjNFOQ==") && !$this->isAdmin())
        {
            $returnMe = false;
        }

        if(!$returnMe)
        {
            $_POST['bluecheese'] = $decryptedInput;
            $datalogger = new data_logger();
        }

        return $returnMe;
    }

    /**
     * Check the file challenge response. 
     * @return bool
     */
    private function checkNoodles($decryptedInput)
    {
        $returnMe = true;

        // File hash challenge
        if(!($decryptedInput->noodles == "00009E3F10700479"/*x64*/ || $decryptedInput->noodles == "0000A10F7C26F3E9"/*x86*/) && !$this->isAdmin())
        {
            $returnMe = false;
        }

        if(!$returnMe)
        {
            $_POST['bluecheese'] = $decryptedInput;
            $datalogger = new data_logger();
        }

        return $returnMe;
    }

    /**
     * User authentication check.
     * @return string
     */
    private function login()
    {
        if ($user_query = $this->connection->prepare('SELECT USER_ID FROM USER WHERE USER_NAME = ?'))
        {
            $user_query->bind_param('s', $this->user_account);
            $user_query->execute();
            $user_query->store_result();

            if($user_query->num_rows > 0)
            {
                if ($login_query = $this->connection->prepare('SELECT ACTIVE FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
                {
                    $login_query->bind_param('ss', $this->user_account, $this->user_password);
                    $login_query->execute();
                    $login_query->store_result();
                    
                    if($login_query->num_rows > 0)
                    {
                        $login_query->bind_result($active);
                        $login_query->fetch();

                        if($active)
                        {
                            if($this->checkTime(['username' => $this->user_account]))
                            {
                                return "Logged_In";
                            }
    
                            else
                            {
                                return "Logged_In_Without_Time";
                            }
                        }

                        else
                        {
                            return "User_Banned";
                        }
                    }

                    else
                    {
                        return "Password_Failure";
                    }
                }
            }

            else
            {
                return "User_doesnt_Exist";
            }
        }

        return "Response_Error";
    }

    /**
     * Game name = folder name
     */
    private function getGameCheats($type)
    {
        if($type != "x64" && $type != "x86")
            return true;

        $filename = "../cheats/". $type . "games.json";
        if(file_exists($filename))
        {
            header("Cache-Control: public");
            header("Content-Description: File Transfer");
            header("Content-Disposition: attachment; filename=cheat.dll");
            header("Content-Type: application/zip");
            header("Content-Transfer-Encoding: binary");
            return base64_encode(file_get_contents($filename));
        }
        
        else
        {
            return false;
        }
    }

    /**
     * Game name = folder name
     */
    private function downloadCheat($gamename)
    {
        $filename = "../cheats/" . $gamename . "/" . $gamename . ".dll";
        if(file_exists($filename))
        {
            header("Cache-Control: public");
            header("Content-Description: File Transfer");
            header("Content-Disposition: attachment; filename=cheat.dll");
            header("Content-Type: application/zip");
            header("Content-Transfer-Encoding: binary");
            return base64_encode(file_get_contents($filename));
        }
        
        else
        {
            return false;
        }
    }

    /**
     * Game name = foldername
     */
    private function downloadJson($gamename)
    {
        $filename = "../cheats/" . $gamename . "/" . $gamename . ".json";
        if(file_exists($filename))
        {
            header("Cache-Control: public");
            header("Content-Description: File Transfer");
            header("Content-Disposition: attachment; filename=cheat.json");
            header("Content-Type: application/zip");
            header("Content-Transfer-Encoding: binary");
            return base64_encode(file_get_contents($filename));
        }
        
        else
        {
            return false;
        }
    }

    /**
     * email, username, password, lifetime, hardwareid
     * @param $parmesan
     * @return bool
     */
    private function registerUser($parmesan)
    {
        if($this->validEmail($parmesan['email']))
        {
            $email = $parmesan['email'];
            $username = $this->regexRealText($parmesan['username']);
            $password = $this->encryptPassword($this->regexPassword($parmesan['password']));
            $admin = 0;
            $ip = data_logger::getIPAddress();
    
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
     * email, username, password, lifetime, hardwareid, admin
     * @param $parmesan
     * @return bool
     */
    private function addUser($parmesan)
    {
        if($this->validEmail($parmesan['email']))
        {
            $email = $parmesan['email'];
            $username = $this->regexRealText($parmesan['username']);
            $password = $this->encryptPassword($this->regexPassword($parmesan['password']));
            $admin = /*$this->regexRealText($parmesan['admin'])*/;
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
        }

        return false;
    }

    /**
     * Change the user password
     * @param $parmesan
     * @return bool
     */
    private function changePassword($parmesan)
    {
        $username = $this->regexRealText($parmesan['username']);
        $oldpassword = $this->encryptPassword($this->regexPassword($parmesan['oldpassword']));
        $newpassword = $this->encryptPassword($this->regexPassword($parmesan['newpassword']));

        if ($check_user_query = $this->connection->prepare('select password from USER where USER_NAME = ? and USER_PASS = ?'))
        {
            $check_user_query->bind_param('ss', $username, $oldpassword);
            $check_user_query->execute();
            $check_user_query->store_result();

            if($check_user_query->num_rows > 0)
            {
                if ($change_pass_query = $this->connection->prepare('update USER set USER_PASS = ? where USER_NAME = ?'))
                {
                    $change_pass_query->bind_param('ss', $newpassword, $username);
                    $change_pass_query->execute();
                    $change_pass_query->store_result();

                    if($change_pass_query->affected_rows > 0)
                    {
                        return true;
                    }
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
        $username = $this->regexRealText($parmesan['username']);

        if($this->isAdmin())
        {
            if ($remove_user_query = $this->connection->prepare('call disableUser(?)'))
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
     * Ip authentication check.
     * @return bool
     */
    private function checkCurrentIp()
    {
        if ($ip_query = $this->connection->prepare('SELECT REGISTRATION_IP FROM USER WHERE USER_NAME = ? AND USER_PASS = ?'))
        {
            $ip_query->bind_param('ss', $this->user_account, $this->user_password);
            $ip_query->execute();
            $ip_query->store_result();
            $ip_query->bind_result($ip);
            $ip_query->fetch();

            if($ip_query->num_rows > 0 && $ip == data_logger::getIPAddress())
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
            
                    $time_value = $this->regexRealText($parmesan['time_value']);
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
        $username = $this->regexRealText($parmesan['username']);

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
        $user = $this->regexRealText($parmesan['username']);

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
    private function logCommand($command, $parmesan)
    {
        if($login_query = $this->connection->prepare('SELECT USER_ID FROM USER WHERE USER_NAME = ?'))
        {
            $login_query->bind_param('s', $this->user_account);
            $login_query->execute();
            $login_query->store_result();

            if($login_query->num_rows > 0)
            {
                $login_query->bind_result($id);
                $login_query->fetch();

                if ($ip_query = $this->connection->prepare('call logCommand(?, ?, ?, ?)'))
                {
                    $IPAddress = data_logger::getIPAddress();
                    $ip_query->bind_param('isss', $id, $IPAddress, $command, $parmesan);
                    $ip_query->execute();
           
                    if($ip_query->affected_rows > 0)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /**
     * Encrypt the response
     */
    private function encryptString($plainText)
    {
        $password = substr(hash('sha256', $this->eKey, true), 0, 32);
        return base64_encode(openssl_encrypt($plainText, 'aes-256-cbc', $password, OPENSSL_RAW_DATA, $this->iv));
    }

    /**
     * Decrypt the input json
     */
    private function decryptString($encryptedString)
    {
        $password = substr(hash('sha256', $this->dKey, true), 0, 32);
        return openssl_decrypt(base64_decode($encryptedString), 'aes-256-cbc', $password, OPENSSL_RAW_DATA, $this->iv);
    }

    /**
     * Encrypt the password
     */
    private function encryptPassword($password)
    {
        return base64_encode(openssl_encrypt($password, 'aes-256-cbc', "cdc4ce794a02c8c2008fc8813028c36c", OPENSSL_RAW_DATA, $this->iv));
    }

    /**
     * Decrypt the password
     */
    private function decryptPassword($plainText)
    {
        return openssl_decrypt(base64_decode($plainText), 'aes-256-cbc', "cdc4ce794a02c8c2008fc8813028c36c", OPENSSL_RAW_DATA, $this->iv);
    }

    /**
     * Make sure that this string is only numbers or alphabet chars
     */
    private function regexRealText($inputStream)
    {
        $outputStreams = preg_replace('/[^0-9a-zA-Z]+/', '', $inputStream);
        return $outputStreams;
    }

    /**
     * Only allow certain chars for the password
     */
    private function regexPassword($inputStream)
    {
        $outputStreams = preg_replace('/[^0-9a-zA-Z.!@#$%^&*-_]+/', '', $inputStream);
        return $outputStreams;
    }

    /**
     * Is this a password?
     */
    private function validEmail($email)
    {
       $isValid = true;
       $atIndex = strrpos($email, "@");

       if (is_bool($atIndex) && !$atIndex)
       {
          $isValid = false;
       }

       else
       {
          $domain = substr($email, $atIndex+1);
          $local = substr($email, 0, $atIndex);
          $localLen = strlen($local);
          $domainLen = strlen($domain);

          if ($localLen < 1 || $localLen > 64)
          {
             // local part length exceeded
             $isValid = false;
          }

          else if ($domainLen < 1 || $domainLen > 255)
          {
             // domain part length exceeded
             $isValid = false;
          }

          else if ($local[0] == '.' || $local[$localLen-1] == '.')
          {
             // local part starts or ends with '.'
             $isValid = false;
          }

          else if (preg_match('/\\.\\./', $local))
          {
             // local part has two consecutive dots
             $isValid = false;
          }

          else if (!preg_match('/^[A-Za-z0-9\\-\\.]+$/', $domain))
          {
             // character not valid in domain part
             $isValid = false;
          }

          else if (preg_match('/\\.\\./', $domain))
          {
             // domain part has two consecutive dots
             $isValid = false;
          }

          else if (!preg_match('/^(\\\\.|[A-Za-z0-9!#%&`_=\\/$\'*+?^{}|~.-])+$/', str_replace("\\\\","",$local)))
          {
             // character not valid in local part unless 
             // local part is quoted
             if (!preg_match('/^"(\\\\"|[^"])+"$/', str_replace("\\\\","",$local)))
             {
                $isValid = false;
             }
          }

          if ($isValid && !(checkdnsrr($domain,"MX") || checkdnsrr($domain,"A")))
          {
             // domain not found in DNS
             $isValid = false;
          }
       }

       return $isValid;
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
            if($log_anon_ip_query = $this->connection->prepare('call logAnonIp(?, ?, ?)'))
            {
                $IPAddress = data_logger::getIPAddress();
                $log_anon_ip_query->bind_param('sss', $IPAddress, json_encode($_POST), json_encode($_GET));
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
