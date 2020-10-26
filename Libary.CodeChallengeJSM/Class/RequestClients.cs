using Newtonsoft.Json;
using Structure.CodeChallengeJSM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Libary.CodeChallengeJSM.Class
{
    public class RequestClients
    {
        #region Request HTTP Json and CSV
        public static List<User> RequestStructureJsonAndCSV(string urlJson, string urlCSV)
        {

            List<User> returnListUsersJson = new List<User>();
            List<User> returnListUsersCSV = new List<User>();
            List<User> returnListAllUsers = new List<User>();
            string readResponseJson;

            #region JSON
            WebRequest request = WebRequest.Create(urlJson);
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                readResponseJson = reader.ReadToEnd();
            }
            response.Close();

            returnListUsersJson = ReturnStructureJsonToStructure(readResponseJson);
            #endregion

            #region CSV

            WebRequest requestCSV = WebRequest.Create(urlCSV);
            WebResponse responseCSV = requestCSV.GetResponse();
            using (Stream dataStream = responseCSV.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                returnListUsersCSV = ReturnStructureCSVToStructure(reader);
            }
            responseCSV.Close();

            #endregion

            #region Concat Lists and Distinct Clients

            returnListAllUsers = returnListUsersCSV.Concat(returnListUsersJson).ToList();
            var distincClientstList = returnListAllUsers.GroupBy(i => i.email).Select(g => g.First()).ToList();

            #endregion

            return distincClientstList;
        }
        #endregion

        #region Json and CSV to Structure
        protected static List<User> ReturnStructureJsonToStructure(string Json)
        {
            StructureReturnJson structureReturnJson = JsonConvert.DeserializeObject<StructureReturnJson>(Json);
            try
            {
                List<User> returnListUsers = new List<User>();
                User returnUser = new User();


                foreach (var item in structureReturnJson.results)
                {
                    returnUser = new User();
                    returnUser.type = typeOfClient(Convert.ToDouble(item.location.coordinates.latitude.Replace(".", ",")), Convert.ToDouble(item.location.coordinates.longitude.Replace(".", ",")));
                    returnUser.gender = item.gender.Substring(0, 1);
                    returnUser.name = new StructureCommon.Name();
                    returnUser.name.title = item.name.title;
                    returnUser.name.first = item.name.first;
                    returnUser.name.last = item.name.last;
                    returnUser.location = new StructureCommon.Location();
                    returnUser.location.region = stateOfClient(item.location.state);
                    returnUser.location.street = item.location.street;
                    returnUser.location.city = item.location.city;
                    returnUser.location.state = item.location.state;
                    returnUser.location.postcode = item.location.postcode;
                    returnUser.location.coordinates = new StructureCommon.Coordinates();
                    returnUser.location.coordinates.latitude = item.location.coordinates.latitude.Replace(".", ",");
                    returnUser.location.coordinates.longitude = item.location.coordinates.longitude.Replace(".", ",");
                    returnUser.location.timezone = new StructureCommon.Timezone();
                    returnUser.location.timezone.offset = item.location.timezone.offset;
                    returnUser.location.timezone.description = item.location.timezone.offset;
                    returnUser.email = item.email;
                    returnUser.birthday = item.dob.date;
                    returnUser.registered = item.registered.date ;
                    returnUser.telephoneNumbers = new List<string>();
                    returnUser.telephoneNumbers.Add(formatPhone(item.phone));
                    returnUser.mobileNumbers = new List<string>();
                    returnUser.mobileNumbers.Add(formatPhone(item.phone));
                    returnUser.picture = new StructureCommon.Picture();
                    returnUser.picture.large = item.picture.large;
                    returnUser.picture.medium = item.picture.medium;
                    returnUser.picture.thumbnail = item.picture.thumbnail;
                    returnUser.nationality = "BR";
                    if (StructureValidator(returnUser))
                           returnListUsers.Add(returnUser);
                }                      
                return returnListUsers;
            }
            catch
            {
                return null;
            }            
        }
        protected static List<User> ReturnStructureCSVToStructure(StreamReader CSV)
        {
            try
            {
                List<User> returnListUsers = new List<User>();
                User returnUser = new User();

                string line = string.Empty;
                using (StreamReader reader = CSV)
                {
 
                    reader.ReadLine().Skip(1);
                    while ((line = reader.ReadLine()) != null)
                    {

                        string[] array = line.Split("\",\"");
                        returnUser = new User();
                        returnUser.type = typeOfClient(Convert.ToDouble(array[8].Replace(".", ",")), Convert.ToDouble(array[9].Replace(".", ",")));
                        returnUser.gender = array[0].Replace("\"","").Substring(0, 1);
                        returnUser.name = new StructureCommon.Name();
                        returnUser.name.title = array[1];
                        returnUser.name.first = array[2];
                        returnUser.name.last = array[3];
                        returnUser.location = new StructureCommon.Location();
                        returnUser.location.region = stateOfClient(array[6]);
                        returnUser.location.street = array[4];
                        returnUser.location.city = array[5];
                        returnUser.location.state = array[6];                        
                        returnUser.location.postcode = Convert.ToInt32(array[7]);
                        returnUser.location.coordinates = new StructureCommon.Coordinates();
                        returnUser.location.coordinates.latitude = array[8].Replace(".", ",");
                        returnUser.location.coordinates.longitude = array[9].Replace(".", ",");
                        returnUser.location.timezone = new StructureCommon.Timezone();
                        returnUser.location.timezone.offset = array[10];
                        returnUser.location.timezone.description = array[11];
                        returnUser.email = array[12];
                        returnUser.birthday = Convert.ToDateTime(array[13]);
                        returnUser.registered = Convert.ToDateTime(array[15]);
                        returnUser.telephoneNumbers = new List<string>();
                        returnUser.telephoneNumbers.Add(formatPhone(array[17]));
                        returnUser.mobileNumbers = new List<string>();
                        returnUser.mobileNumbers.Add(formatPhone(array[18]));
                        returnUser.picture = new StructureCommon.Picture();
                        returnUser.picture.large = array[19];
                        returnUser.picture.medium = array[20];
                        returnUser.picture.thumbnail = array[21].Replace("\"", "");
                        returnUser.nationality = "BR";

                        if (StructureValidator(returnUser))
                            returnListUsers.Add(returnUser);
                    }
                }

                return returnListUsers;
            }
            catch
            {
                return null;
            }            
        }
        #endregion

        #region Useful methods
        protected static string typeOfClient (double lat, double lon)
        {
            if ((lat > -46.361899 && lat < -34.276938) && (lon > -15.411580 && lon < -2.196998))
            {
                return "special";
            }
            if ((lat > -52.997614 && lat < -44.428305) && (lon > -23.966413 && lon < -19.766959))
            {
                return "special";
            }
            if ((lat > -54.777426 && lat < -46.603598) && (lon > -34.016466 && lon < -26.155681))
            {
                return "normal";
            }
            else
                return "laborious";
        }
        protected static string stateOfClient(string state)
        {
            string newState = RemoveAccents(state).ToLower();
            string region = "";
            switch(newState)
            {
                case "espirito santo":
                case "minas gerais":
                case "sao paulo":
                case "rio de janeiro":
                      region = "southeast";
                    break;

                case "rio grande do sul":
                case "parana":
                case "santa catarina":                
                    region = "south";
                    break;

                case "goias":
                case "mato grosso":
                case "mato grosso do sul":
                case "distrito federal":
                    region = "midwest";
                    break;

                case "acre":
                case "amazonas":
                case "amapa":
                case "para":
                case "rondonia":
                case "roraima":
                case "tocantins":
                    region = "north";
                    break;

                case "alagoas":
                case "bahia":
                case "paraiba":
                case "maranhao":
                case "piaui":
                case "pernambuco":
                case "rio grande do norte":
                case "sergipe":
                    region = "northeast";
                    break;

            }
            return region;
        }
        protected static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
        protected static string formatPhone(string phone)
        {
            string newPhone = "+55" + phone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            return newPhone;
        }
        protected static bool StructureValidator(User user)
        {
            try
            {
                var email = new EmailAddressAttribute();
                bool validate = email.IsValid(user.email);

                if (!validate)
                {
                    return false;
                }

                if (user.gender.ToUpper() == "F" || user.gender.ToUpper() == "M")
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
