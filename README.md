# 练习项目

https://github.com/dragonety/FunnyBattle

## 简介

此项目的主要目的在于熟悉UNet的通信流程，练习游戏编程模式与常用的架构方法，本次项目学习的过程如下：

1. 学习示例项目的结构
2. 手动创建学习UNet的工程
3. 重构以练习不同的游戏编程模式
4. 在工程上学习并使用AssetBundle
5. 完善当前项目的流程和架构

当前在过程3

## 项目功能

- 管理模块：
  - GameManager做一些数据上的同步，当前的作用是记录
  - EffectManager特效的管理
- 资源管理：待完成
- 战斗玩法：
  - ~~PlayerController包含所有角色控制的内容~~
  - PlayerEntity作为一个player实例，使用“组件模式”将player上的功能拆分
  - 使用“消息队列模式”来传递消息
    - PlayerEntity本身作为NetworkBehaviour来通过SyncVar同步数据
      - 同步的数据包括position rotation health
    - PlayerMoveComponent用来通过input信息控制player移动
      - WASD移动
      - 只有local的player使用
    - PlayerNetMoveComponent通过同步的位置信息控制player移动
      - 非local的player通过lerp同步的位置信息移动
    - PlayerGraphicComponent控制player动画表现
    
      - 动画信息使用NetworkAnimator同步
      - 左键攻击，右键格挡，E发射魔法球
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

## 学习日志

5.19

学习了“组件模式”，应用到了playerController，在应用过程中发现拆分后到component没有合适的消息传播机制，于是学习了“事件队列模式”来处理消息的注册与传递（未完成）

