namespace RageTrailersSync.Server
{
    class IdGenerator
    {
        private uint _lastGeneratedId;
        
        public IdGenerator(uint id = 0)
        {
            _lastGeneratedId = id;
        }

        public void Set(uint value)
        {
            _lastGeneratedId = value;
        }

        public uint Generate()
        {
            return _lastGeneratedId++;
        }
    }
}
