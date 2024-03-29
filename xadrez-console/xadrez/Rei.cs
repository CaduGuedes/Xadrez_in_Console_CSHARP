﻿using tabuleiro;

namespace xadrez {
    class Rei : Peca {


        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) {
            this.partida = partida;
        }

        public override string ToString() {
            return "R";
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor; //retorna se nn tem peça na posicao ou a peca eh adversaria
        }

        private bool testeTorreParaRoque(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qtdMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis() { //override pq ta subscrevendo metodo abstract da classe peca
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            // verficando acima 
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // verficando nordeste 
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // verficando direita 
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // verficando sudeste 
            pos.definirValores(posicao.linha + 1, posicao.coluna +1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // verficando abaixo 
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // verficando sudoeste 
            pos.definirValores(posicao.linha + 1, posicao.coluna -1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // verficando esquerda 
            pos.definirValores(posicao.linha, posicao.coluna -1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // verficando noroeste 
            pos.definirValores(posicao.linha - 1, posicao.coluna -1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //#JogadaEspecial ROQUE 
            if (qtdMovimentos == 0 && !partida.xeque) {
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posT1)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if(tab.peca(p1) == null && tab.peca(p2) == null) {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }
            }

            //Roque GRANDE
            if (qtdMovimentos == 0 && !partida.xeque) {
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posT2)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }

            return mat;
        }

    }
}
