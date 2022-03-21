using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServiceAppWMB.Board
{
    public class BoardProcessor : IBoardProcessor
    {
        public void GetAllBoardingCustomer()
        {
            throw new NotImplementedException();
        }

        public bool OnBoardCustomer(boardDTO boardDTO)
        {
            if(boardDTO.Email == null || boardDTO.Email == "")
            {
                throw new ArgumentOutOfRangeException("Email cannot be null or empty");
            }
            if (boardDTO.Password == null || boardDTO.Password == "")
            {
                throw new ArgumentOutOfRangeException("Password cannot be null");
            }
            
            if (boardDTO.PhoneNumber == null || boardDTO.PhoneNumber == "")
            {
                throw new ArgumentOutOfRangeException("PhoneNumber cannot be null or less than 11");
            }

            return true;
        }
    }
}
