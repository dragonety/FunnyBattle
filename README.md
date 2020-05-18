# 练习项目

## 简介

此项目的主要目的在于熟悉UNet的通信流程，由于示例工程包含内容很广也很完善，因此希望在学习UNet的同时也学习其他的模块以及架构方法，故而本次项目学习的过程如下：

1. 学习示例项目的结构
2. 手动创建学习UNet的工程
3. 在工程上学习并使用AssetBundle
4. 完善当前项目的流程和架构

当前在过程2

## 项目功能

- 管理模块：
  - GameManager做一些数据上的同步，当前的作用是记录
  - EffectManager特效的管理
- 资源管理：待完成
- 战斗玩法：
  - PlayerController包含所有角色控制的内容
    - 需要同步position、rotation
      - 本地玩家按照角色控制位移
      - 非本地玩家按照同步来的位置信息lerp
    - 需要同步health
      - health发生变化时需要同步变更healthBar
    - player根据isMoving等标志
    - player发射技能的时候需要向gameManager记录发射物与发射人的dict
  - MagicController包含技能的碰撞检测
    - 需要同步velocity
    - 在发生碰撞的时候需要从gameManager查询是否接触到的是技能释放者

## 游戏流程

### Host：

进入login场景

点击login进入lobby场景

点击host创建游戏，进入game场景

正常游玩，游戏结束返回lobby

ESC打开UI

点击stopHost关闭并返回lobby场景

### Client:

进入login场景

点击login进入lobby场景

点击client,在有host的情况下可以进入game场景

正常游玩，游戏结束返回lobby

ESC打开UI

点击stopClient关闭并返回lobby场景

