**Author:** *Mark Peters, Senior Software Engineer*

**Abstract:**
The purpose of this project is to demonstrate how you can use multiple sorters, 
using design patterns (factory and strategy), while honoring the SOLID principles.
My goal was to make this project 'as-readable' as it can be, and in order to accomplish this, I used fluent coding.

**Implementation:**<br/>
![architecture](https://github.com/mpeters21575/Acme.Sorter/blob/master/architecture.PNG)

**Sorters:**<br/>
* Bubble sorter: Sorts the text file using the bubble sort algortihm<br/>
* Alphabetic sorter: Only sorts the file ascending<br/>
* Length sorter: Sorts the text file from the smallest sentence to the largest<br/>

**Framework**: netcore 3.1<br/>
**Dependencies:** Simple Injector<br/>
**Unit testing:** XUnit<br/>

**Tests:**
I tried to aim for 100% code coverage, but for this demo project I don't test the front-end. Only the buisiness logic, and extention methods are tested.
Also pay attention, that I choose to use a textfile that contains a Russian poem to prove that the output is compliant with any culture that is configured on the target system. (UTF-8)
