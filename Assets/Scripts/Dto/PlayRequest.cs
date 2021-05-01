namespace Dto
{
    [System.Serializable]
    public class PlayRequest
    {
        public string game;

        public PlayRequest(string game) {
            this.game = game;
        }
    }
}