using System;

namespace MicroServiceAppWMB.Board
{
    public interface IBoardProcessor
    {
        void GetAllBoardingCustomer();
        bool OnBoardCustomer(boardDTO boardDTO);
    }
}
