# CryptoFolio
This project was built solely with the purpose of practicing my web development skills. The project is composed by 3 sub-projects, WebAPI and WorkerService built with C# on .Net Core and WebApp built with HTML, CSS and Javascript on React.

# WebAPI
Consists of 4 controllers with CRUD operations for User, Wallet, Coin and Networth models. Built with Entity Framework and SQL server.

# WorkerService
The worker service is constantly updating the user's networth and the coin prices trought calls on nomic's public API.

# WebApp
The WebApp contains 5 pages:
1. Home: Where the top 10 coins by Market Cap are displayed.
2. Coins: Where The top 1000 coins by MarketCap are displayed on pages containing 100 coins each.
3. Register: Where the user can register a new account.
4. Login: Where the user can login into it's account.
5. Wallet: App's main funcionality page, where the user can add/edit/delete new coins into the wallet and track the growth by a line graphic.

Tools used:

- React router
- Axios
- MaterialUI
- Highcharts
- Flexbox
