namespace GraphicalCalculatorNEA
{
    class Node
    {
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
    class Tree
    {
        Node root = null;
        Node currnode = null;

    }
    class Lexer
    {
        char currchar;
        Token currtok = new Token(TokenType.LParen, null);
        int pos;
        string input;

        public Lexer(string Input)
        {
            input = Input;
            pos = -1;
            input = RemoveWhitespace(input);
            input = Negatives(input);
            CheckBracketPairs();

        }
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
        private void CheckEnd()
        {
            if (pos + 1 == input.Length)
            {
                currtok.type = TokenType.Error;
                currtok.value = "ERROR: Expression unrecognised.";
            }
        }
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
    class Parser
    {
        public Node root = new Node(null, null, null);
        Node node = new Node(null, null, null);
        Token currtok;
        Lexer lexer;
        bool error = false;
        public Parser(string input)
        {
            lexer = new Lexer(input);
            currtok = lexer.GetNextToken();
            root = Expression();
        }
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
        private void Consume()
        {
            currtok = lexer.GetNextToken();
        }
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
                    temp.right = Factor();
                    if (error == true)
                    {
                        node = temp.right;
                    }
                    else
                    {
                        node = temp;
                    }
                    node = temp;
                }
                if (currtok.type == TokenType.Log)
                {
                    Node temp = new Node(null, null, currtok.value);
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
                if (currtok.type == TokenType.LParen)
                {
                    Consume();
                    node = Expression();
                    Consume();
                }
                if (currtok.type == TokenType.Error)
                {
                    node = new Node(null, null, currtok.value);
                    error = true;
                    currtok.type = TokenType.End;
                }
            } 
            return node;
        }
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
                    node.value = Convert.ToString(Math.Cos(Convert.ToDouble(right)));
                    return node;
                case "sin":
                    node.value = Convert.ToString(Math.Sin(Convert.ToDouble(right)));
                    return node;
                case "tan":
                    node.value = Convert.ToString(Math.Tan(Convert.ToDouble(right)));
                    return node;
                case "ln":
                    node.value = Convert.ToString(Math.Log(Convert.ToDouble(right)));
                    return node;
            }
            return node;
        }
    }
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Graph());
        }
    }
}