using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerInterface;

namespace MONOPOLY
{
    public class HumanPlayer : Player
    {
        public HumanPlayer()
        {
            items = new List<int>();
        }

        private EDecision PromptYesNoDecision(int cost)
        {
            UI.EDecision decision = ui.PromptYesNoDecision(cost);

            if (decision == UI.EDecision.YES)
                return EDecision.YES;
            else
                return EDecision.NO;
        }

        private EJailDecision PromptJailDecision()
        {
            UI.EJailDecision decision = UI.GetJailDecision();
            switch (decision)
            {
                case UI.EJailDecision.PAY:
                    return EJailDecision.PAY;
                case UI.EJailDecision.ROLL:
                    return EJailDecision.ROLL;
                case UI.EJailDecision.CARD:
                    return EJailDecision.CARD;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }

        public override EDecision DecideBuy(int index)
        {
            return PromptYesNoDecision(Board.COSTS[index]);
        }

        public override EJailDecision DecideJail()
        {
            return PromptJailDecision();
        }

        public override EDecision DecideMortgage(int index)
        {
            return EDecision.YES;
        }

        public override EDecision DecideAdvance(int index)
        {
            return EDecision.YES;
        }

        public override EDecision DecideBuildHouse(int index)
        {
            return PromptYesNoDecision();
        }
    }
}
