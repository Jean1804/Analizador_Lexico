using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizador_Lexico
{
    class Lexico
    {
        const int TOKREC = 5;
        const int MAXTOKENS = 500;
        string[] _lexemas;
        string[] _tokens;
        string _lexema;
        int _noTokens;
        int _i;
        int _iniToken;
        Automata oAFD;

        public Lexico() 
        {
            _lexemas = new string[MAXTOKENS];
            _tokens = new string[MAXTOKENS];
            oAFD = new Automata();
            _i = 0;
            _iniToken = 0;
            _noTokens = 0;
        }

        public int NoTokens 
        {
            get { return _noTokens; }
        }
        public string[] Lexema 
        {
            get { return _lexemas; }
        }
        public string[] Token 
        {
            get { return _tokens; }        
        }
        public void Inicia() 
        {
            _i = 0;
            _iniToken = 0;
            _noTokens = 0;
        }

        public void Analiza(string texto) 
        {
            bool recAuto;
            int noAuto;
            while(_i < texto.Length) 
            {
                recAuto = false;
                noAuto = 0;
                for (; noAuto < TOKREC && !recAuto;)
                    if (oAFD.Reconoce(texto, _iniToken, ref _i, noAuto))
                        recAuto = true;
                    else
                        noAuto++;
                if (recAuto)
                {
                    _lexema = texto.Substring(_iniToken, _i - _iniToken);

                    switch (noAuto)
                    {
                        case 0:
                            break;
                        case 1: if (EsId())
                                _tokens[_noTokens] = "id";
                            else
                                _tokens[_noTokens] = _lexema;
                            break;

                        case 2: _tokens[_noTokens] = "num";
                            break;

                        case 3:
                            _tokens[_noTokens] = _lexema;
                            break;
                        case 4:
                            _tokens[_noTokens] = "cad";
                            break;
                    }
                    if (noAuto != 0)
                        _lexemas[_noTokens++] = _lexema;
                }
                else
                    _i++;
                    _noTokens = _i;
            }
        }

        private bool EsId() 
        {
            string[] palres = { "inicio", "fin", "const", "var", "entero", "real", "cadena", "leer", "visua" };
            for (int i = 0; i < palres.Length; i++)
                if (_lexema == palres[i])
                    return false;
                    return true;
        }
    }
}
