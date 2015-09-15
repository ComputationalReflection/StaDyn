using antlr;

namespace Parser
{
    public interface ICSharpLexer : TokenStream
    {
        TokenStreamSelector Selector { get; set; }
        void setFilename(string f);
        IToken nextToken();
        LexerSharedInputState getInputState();
        void setTabSize(int size);
        void setTokenCreator(TokenCreator tokenCreator);
    }
}