using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerInterface;

namespace MONOPOLY
{
    public class Player
    {
        public enum EState
        {
            NORMAL,
            JAIL,
            RETIRED,
        }

        public enum EJailDecision
        {
            ROLL,
            PAY,
            CARD
        }

        public enum EDecision
        {
            YES,
            NO
        }

        public EState state = EState.NORMAL;

        public int position = 0;
        public int funds = 1500;

        public int jail = 0;
        public int doub = 0;
        public int card = 0;

        public List<int> items;

        public PlayerUI player_ui;
        public UI ui;

        public Player()
        {
            items = new List<int>();
        }

        public virtual EDecision DecideBuy(int index)
        {
            return EDecision.YES;
        }

        public virtual EJailDecision DecideJail()
        {
            return EJailDecision.ROLL;
        }

        public virtual EDecision DecideMortgage(int index)
        {
            if (funds < 0)
            {
                return EDecision.YES;
            }

            return EDecision.NO;
        }

        public virtual EDecision DecideAdvance(int index)
        {
            return EDecision.YES;
        }

        public virtual EDecision DecideBuildHouse(int index)
        {
            return EDecision.YES;
        }

        public virtual int DecideSellHouse(int set)
        {
            if (funds < 0)
            {
                return 15;
            }

            return 0;
        }

        public virtual void Move(int roll)
        {
            player_ui.Move(position, roll);
        }

        public void Purchase(int index)
        {
            player_ui.Purchase(index);
        }
    }
}
