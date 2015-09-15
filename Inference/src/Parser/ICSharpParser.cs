using AST;

namespace Parser
{
    public interface ICSharpParser
    {
        void setFilename(string filename);
        SourceFile compilationUnit();
    }
}