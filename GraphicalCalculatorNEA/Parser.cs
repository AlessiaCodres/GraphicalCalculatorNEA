using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalCalculatorNEA
{
    // Node class is used to create objects that make up the expression tree 
    class Node
    {
        // left and right pointers used to connect nodes to their child nodes all the way down the tree
        public Node left = null;
        public Node right = null;
        public string value = "";
        public Node(Node Left, Node Right, string Value)
        {
            left = Left;
            right = Right;
            value = Value;
        }
    }
    // enumerated type TokenType is used to assign a token a category of symbol that can be found in a valid expression
    // with the addition of an error and the end of the expression being reached
    enum TokenType
    {
        LParen,
        RParen,
        Operator1,
        Operator2,
        Exp,
        Trig,
        Log,
        Num,
        Var,
        Const,
        End,
        Error
    }
    // token class used to divide the expression input by the user into individual units e.g. numbers, variables etc.
    class Token
    {
        public TokenType type;
        public string value;

        public Token(TokenType Type, string Value)
        {
            type = Type;
            value = Value;
        }
    }
    // lexer class is used to divide the expression into tokens
    class Lexer
    {
        private char currchar;
        private Token currtok = new Token(TokenType.LParen, null);
        private int pos;
        private string input;

        public Lexer(string Input)
        {
            input = Input;
            pos = -1;
            input = RemoveWhitespace(input);
            input = Negatives(input);
            DoubleOperator(input);
            CheckBracketPairs();

        }
        // any empty space is removed from the expression
        private string RemoveWhitespace(string input)
        {
            string temp = input;
            input = "";
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != ' ')
                {
                    input = input + temp[i].ToString();
                }
            }
            return input;
        }
        // if an expression begins with -, it is replaced with 0-, making it easier to process and evaluate
        // any double negatives are replaced with a +
        private string Negatives(string input)
        {
            if (input[0] == '-')
            {
                input = "0" + input;
            }
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i - 1] == '-' && input[i] == '-')
                {
                    input = input.Substring(0, i - 1) + "+" + input.Substring(i + 1);
                }
            }
            return input;
        }
        // checks whether all brackets have a pair - if not an error returned
        public void CheckBracketPairs()
        {
            int LCount = 0;
            int RCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    LCount++;
                }
                if (input[i] == ')')
                {
                    RCount++;
                }
            }
            if (LCount != RCount)
            {
                currtok.value = "ERROR: Bracket pair missing.";
                currtok.type = TokenType.Error;
                currchar = '!';
            }
        }
        // checks if there are invalid double operators
        private void DoubleOperator(string input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                if ((input[i] == '+' || input[i] == '*' || input[i] == '/') && input[i] == input[i + 1])
                {
                    currtok.value = "ERROR: Double Operators not accepted.";
                    currtok.type = TokenType.Error;
                    currchar = '!';
                }
            }
        }
        // the next character of the expression is set to be the current character 
        private void Next()
        {
            pos++;
            if (pos == input.Length)
            {
                currchar = ';';
            }
            else
            {
                currchar = input[pos];
            }
        }
        // certain characters cannot be at the end of the expression as it would not make sense, this checks whether this has happened and returns an error if that is the case
        // e.g. *, /, ( etc.
        private void CheckEnd()
        {
            if (pos + 1 == input.Length)
            {
                currtok.type = TokenType.Error;
                currtok.value = "ERROR: Expression unrecognised.";
            }
        }
        // the next token is obtained by updating the current character and identifying what type of token it is
        public Token GetNextToken()
        {
            while (currchar != '!')
            {
                TokenType type_ = currtok.type;
                Next();
                if (char.IsDigit(currchar) || currchar == '.')
                {
                    currtok = GetNumber();
                }
                else if (char.IsLetter(currchar))
                {
                    currtok = GetWord();
                }
                else if (currchar == '(')
                {
                    currtok = new Token(TokenType.LParen, "(");
                    CheckEnd();
                }
                else if (currchar == ')')
                {
                    currtok = new Token(TokenType.RParen, ")");
                }
                else if (currchar == '^')
                {
                    currtok = new Token(TokenType.Exp, "^");
                    CheckEnd();
                }
                else if (currchar == '*' || currchar == '/')
                {
                    currtok = new Token(TokenType.Operator1, currchar.ToString());
                    CheckEnd();
                }
                else if (currchar == '+' || currchar == '-')
                {
                    currtok = new Token(TokenType.Operator2, currchar.ToString());
                    CheckEnd();
                }
                else if (currchar == ';')
                {
                    currtok = new Token(TokenType.End, ";");
                }
                else
                {
                    currtok = new Token(TokenType.Error, currchar.ToString());
                }
                // handles the cases where an * is omitted e.g. 3x, 3cos(x), 4(x+5) etc.
                if ((type_ == TokenType.Num || type_ == TokenType.Var || type_ == TokenType.Const || type_ == TokenType.RParen) &&
                    (currtok.type == TokenType.Var || currtok.value == "e" || currtok.type == TokenType.LParen))
                {
                    currtok = new Token(TokenType.Operator1, "*");
                    pos--;
                }
                if ((type_ == TokenType.Num || type_ == TokenType.Var || type_ == TokenType.Const || type_ == TokenType.RParen) &&
                    (currtok.type == TokenType.Trig))
                {
                    currtok = new Token(TokenType.Operator1, "*");
                    pos -= 3;
                }
                if ((type_ == TokenType.Num || type_ == TokenType.Var || type_ == TokenType.Const || type_ == TokenType.RParen) &&
                    (currtok.value == "pi" || currtok.type == TokenType.Log))
                {
                    currtok = new Token(TokenType.Operator1, "*");
                    pos -= 2;
                }
                if (currtok.type == TokenType.Error)
                {
                    if (currtok.value != "ERROR: Expression unrecognised.")
                    {
                        currtok.value = "ERROR: " + currtok.value + " not recognised.";
                    }
                }
                return currtok;
            }
            return currtok;
        }
        // when a digit is encountered in the expression, the whole number must be treated as one token, GetNumber() handles this
        private Token GetNumber()
        {
            string num = Convert.ToString(currchar);
            if (pos == input.Length - 1)
            {
                return new Token(TokenType.Num, num);
            }
            else
            {
                while (pos + 1 < input.Length)
                {
                    if (input[pos + 1] == '.' || char.IsDigit(input[pos + 1]))
                    {
                        Next();
                        if (currchar == '!')
                        {
                            break;
                        }
                        num = num + Convert.ToString(currchar);
                    }
                    else
                    {
                        return new Token(TokenType.Num, num);
                    }
                }
            }
            return new Token(TokenType.Num, num);
        }
        // when a letter is encountered in the expression, it must be identified whether it is a single letter or a string as multiple cases are valid e.g. x, sin etc.
        private Token GetWord()
        {
            string word = Convert.ToString(currchar);
            while (pos + 1 < input.Length)
            {
                if (char.IsLetter(input[pos + 1]))
                {
                    Next();
                    if (currchar == '!')
                    {
                        break;
                    }
                    word = word + Convert.ToString(currchar);
                }
                else
                {
                    break;
                }
            }
            if (word == "sin" || word == "cos" || word == "tan")
            {
                return new Token(TokenType.Trig, word);
            }
            if (word == "x")
            {
                return new Token(TokenType.Var, word);
            }
            if (word == "e")
            {
                return new Token(TokenType.Const, Convert.ToString(Math.Exp(1)));
            }
            if (word == "pi")
            {
                return new Token(TokenType.Const, Convert.ToString(Math.PI));
            }
            if (word == "ln")
            {
                return new Token(TokenType.Log, word);
            }
            return new Token(TokenType.Error, word);
        }
    }
    // the parser class the lexer to build an expression tree made of nodes from the inputted expression, and evaluate it 
    internal class Parser
    {
        public Node root = new Node(null, null, null);
        private Node node = new Node(null, null, null);
        private Token currtok;
        private Lexer lexer;
        private bool error = false;
        public Parser(string input)
        {
            lexer = new Lexer(input);
            currtok = lexer.GetNextToken();
            // Expression() is called to start the recursive process of building the tree
            // once complete, the tree will be returned by returning the root node, with pointers to the child nodes all the way down
            root = Expression(); 
        }
        // brackets are necessary in certain parts of expressions for clarity (e.g. cos(x), e^(3x) etc. ), if not present, an error is returned to the user
        private void CheckBracket()
        {
            if (currtok.type == TokenType.LParen)
            {
                return;
            }
            else
            {
                currtok.value = "ERROR: Bracket expected.";
                currtok.type = TokenType.Error;
            }
        }
        // the next token is fetched from the lexer
        private void Consume()
        {
            if (!error)
            {
                currtok = lexer.GetNextToken();
            } 
        }
        // Expression(), Term() and Factor() call each other recursively depending on operator precedence to maintain order of operations in the tree created
        // the expression is broken down into terms at this point by identifying operators of second precedence + and -
        private Node Expression()
        {
            Node node = Term();

            while (currtok.type == TokenType.Operator2)
            {
                Node temp = new Node(node, null, currtok.value);
                Consume();
                temp.right = Term();
                if (error == true)
                {
                    node = temp.right;
                }
                else
                {
                    node = temp;
                }
            }
            return node;
        }
        // the terms are broken down into factors at this point by identifying operators of first precedence * and /
        private Node Term()
        {
            Node node = Factor();

            while (currtok.type == TokenType.Operator1)
            {
                Node temp = new Node(node, null, currtok.value);
                Consume();
                temp.right = Factor();
                if (error == true)
                {
                    node = temp.right;
                }
                else
                {
                    node = temp;
                }
            }
            return node;
        }
        // the factors are broken down into individual units like numbers, functions, variables and constants here
        // this is done by identifying these as well as any brackets and exponential signs 
        private Node Factor()
        {
            while (currtok.type == TokenType.Num || currtok.type == TokenType.Var || currtok.type == TokenType.Trig || currtok.type == TokenType.Log
                || currtok.type == TokenType.Exp || currtok.type == TokenType.Const || currtok.type == TokenType.LParen || currtok.type == TokenType.Error)
            {

                if (currtok.type == TokenType.Num || currtok.type == TokenType.Var || currtok.type == TokenType.Const)
                {
                    node = new Node(null, null, currtok.value);
                    Consume();
                }
                if (currtok.type == TokenType.Trig)
                {
                    Node temp = new Node(null, null, currtok.value);
                    Consume();
                    CheckBracket();
                    temp.left = Factor();
                    temp.right = temp.left;
                    if (error == true)
                    {
                        node = temp.left;
                    }
                    else
                    {
                        node = temp;
                    }
                }
                if (currtok.type == TokenType.Log)
                {
                    Node temp = new Node(null, null, currtok.value);
                    Consume();
                    CheckBracket();
                    temp.left = Factor();
                    temp.right = temp.left;
                    if (error == true)
                    {
                        node = temp.left;
                    }
                    else
                    {
                        node = temp;
                    }
                    node = temp;
                }
                if (currtok.type == TokenType.Exp)
                {
                    Node temp = new Node(node, null, currtok.value);
                    Consume();
                    CheckBracket();
                    temp.right = Factor();
                    if (error == true)
                    {
                        node = temp.right;
                    }
                    else
                    {
                        node = temp;
                    }
                }
                // all contents of brackets are treated as one separate expression that is converted to its own tree that can be added in the expression tree where approapriate
                if (currtok.type == TokenType.LParen) 
                {
                    Consume();
                    node = Expression();
                    Consume();
                }
                // if an error takes place, a single node containing this error is returned
                if (currtok.type == TokenType.Error)
                {
                    node = new Node(null, null, currtok.value);
                    error = true;
                    currtok.type = TokenType.End;
                }
            }
            return node;
        }
        // converts from radians to degrees if the user has selected degrees in the settings instead of radians 
        private double GetTrigValue(double value)
        {
            StreamReader reader = new StreamReader("Settings.txt");
            for (int i = 0; i < 4; i++)
            {
                reader.ReadLine();
            }
            string angletype = reader.ReadLine();
            reader.Close();
            if (angletype == "Degrees")
            {
                return Convert.ToDouble(value * (Math.PI/180));
            }
            else
            {
                return value;
            }
        }
        // the expression tree is evaluated to produce a corresponding y coordinate to the inputted x coordinate
        // recursion is used evaluating the tree from the leaf nodes up by calling Evaluate() until the terminal node is reached
        public Node Evaluate(Node node, string x)
        {
            if (error == true)
            {
                return node;
            }
            if (node.left == null && node.right == null)
            {
                if (node.value == "x")
                {
                    node.value = x;
                }
                return node;
            }
            string left = "";
            if (node.left != null)
            {
                while (node.left.left != null)
                {
                    node.left = Evaluate(node.left, x);
                }
                left = node.left.value;
            }
            if (left == "x")
            {
                left = x;
            }
            string right = "";
            if (node.right != null)
            {
                while (node.right.right != null)
                {
                    node.right = Evaluate(node.right, x);
                }
                right = node.right.value;
            }
            if (right == "x")
            {
                right = x;
            }
            node.right = null;
            node.left = null;
            switch (node.value)
            {
                case "+":
                    node.value = Convert.ToString(Convert.ToDouble(left) + Convert.ToDouble(right));
                    return node;
                case "-":
                    node.value = Convert.ToString(Convert.ToDouble(left) - Convert.ToDouble(right));
                    return node;
                case "*":
                    node.value = Convert.ToString(Convert.ToDouble(left) * Convert.ToDouble(right));
                    return node;
                case "/":
                    node.value = Convert.ToString(Convert.ToDouble(left) / Convert.ToDouble(right));
                    return node;
                case "^":
                    node.value = Convert.ToString(Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right)));
                    return node;
                case "cos":
                    node.value = Convert.ToString(Math.Cos(GetTrigValue(Convert.ToDouble(left))));
                    return node;
                case "sin":
                    node.value = Convert.ToString(Math.Sin(GetTrigValue(Convert.ToDouble(left))));
                    return node;
                case "tan":
                    node.value = Convert.ToString(Math.Tan(GetTrigValue(Convert.ToDouble(left))));
                    return node;
                case "ln":
                    node.value = Convert.ToString(Math.Log(GetTrigValue(Convert.ToDouble(left))));
                    return node;
            }
            return node;
        }
    }
}

