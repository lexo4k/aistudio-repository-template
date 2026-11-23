# ASTRION — O Último Horizonte

Projeto Unity 3D de aventura e exploração em terceira pessoa. A branch principal do repositório foi preservada; todo o trabalho do jogo está isolado nesta branch: `astrion`.

## Abrir no Unity
1. Clone esta branch do repositório.
2. No Unity Hub, selecione **Add > Add project from disk** e escolha a pasta clonada.
3. Aguarde a importação. Abra uma cena vazia e pressione Play.
4. O protótipo gera o vale, iluminação, personagem e câmera automaticamente.

## Requisitos e controles
- Unity 2022.3 LTS ou mais recente.
- WASD: mover; mouse: câmera; Espaço: pular; Shift: correr; Esc: liberar cursor.

## Estado real
Fundação inicial jogável: cenário procedural, controlador em terceira pessoa e câmera orbital. Missões, NPCs, combate, inventário, áudio, salvamento e campanha ainda não foram implementados. Esta base não é um jogo AAA finalizado e ainda precisa ser aberta, compilada e testada no Editor Unity.

## Estrutura
- `Assets/ASTRION/Scripts`: código de runtime.
- `Assets/ASTRION/Scenes`: cenas.
- `Assets/ASTRION/Art`: arte e materiais.
- `ProjectSettings`: configuração do projeto.
