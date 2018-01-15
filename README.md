# RLNews

***Incomplete / WIP***

For my final year project at University I developed an ASP.NET MVC website called ‘RLNews’ that aggregates rugby league news from around the web using RSS data feeds. The aim of the website is to provide a dynamic user experience for consuming Rugby League news from multiple different sources. The RSS feeds are fetched automatically by a simple console application. The console application first checks that new news items are available, parses the XML data and inserts them into the database.

To prevent multiple instances of the similar articles being displayed in the news feed, the news articles grouped together using two categories: Parent and Child. The Parent articles are the first instance of that news story and the Child article is an article that is about a similar topic and was published within 24 hours of parent. To perform the categorisation, I am using combination of string matching techniques such as Levenshtein distance and the removal of link words to pull out all of the named entities in news article headlines such as ‘Castleford’.

* C# / ASP.NET MVC
* Entity Framework
* Bootstrap 3
* JS / jQuery / AJAX
* NPM / Gulp.JS / Less
* Microsoft SQL Server

### Preview

<img width="865" alt="RLNews homepage" src="https://raw.githubusercontent.com/ahawkin/personal-portfolio/master/assets/img/previews/rlnews-preview-1.PNG">