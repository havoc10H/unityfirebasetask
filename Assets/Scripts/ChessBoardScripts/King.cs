using System.Collections;
using UnityEngine;

public class King : Chessman
{    
    private bool[] Castling = new bool[2];
    public override bool[,] PossibleMoves()
    {
        bool[,] r = new bool[8, 8];

        Move(CurrentX + 1, CurrentY, ref r); // up
        Move(CurrentX - 1, CurrentY, ref r); // down
        Move(CurrentX, CurrentY - 1, ref r); // left
        Move(CurrentX, CurrentY + 1, ref r); // right
        Move(CurrentX + 1, CurrentY - 1, ref r); // up left
        Move(CurrentX - 1, CurrentY - 1, ref r); // down left
        Move(CurrentX + 1, CurrentY + 1, ref r); // up right
        Move(CurrentX - 1, CurrentY + 1, ref r); // down right
        
        // if (isWhite) {
        //     if (OnlineOrLocal.Instance.mode == 1) {
        //         // Castling = OnlineBoardManager.Instance.WhiteCastling;
        //     } else if (OnlineOrLocal.Instance.mode == 0) {
        //         // c = LocalBoardManager.Instance.Chessmans[x, y];
        //     } else {
        //         Castling = AIBoardManager.Instance.WhiteCastling;
        //     }            
        // } else {
        //     if (OnlineOrLocal.Instance.mode == 1) {
        //         // Castling = OnlineBoardManager.Instance.WhiteCastling;
        //     } else if (OnlineOrLocal.Instance.mode == 0) {
        //         // c = LocalBoardManager.Instance.Chessmans[x, y];
        //     } else {
        //         Castling = AIBoardManager.Instance.BlackCastling;
        //     }
        // }

        if (Castling[0]) {
            r[CurrentX - 2, CurrentY] = true;
        }
        if (Castling[1]) {
            r[CurrentX + 2, CurrentY] = true;
        }    

        return r;
    }
}