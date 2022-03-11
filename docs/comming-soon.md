## Here is a list of feature that might big introduced in the library: 

## 1. Validators
A list of built in feature checks that could check for the integrity of the injection or for the possible states of the design patterns use
For example a check for cycle dependency between the injected services. This should check the implementation not the abstractions.

## 2. Waterfall
This should be a more advance way to handle requests.
A class that afert receive a requests similiar to the way MediatR works would actually allow the developer to return another type that could be a request or a response
If the type is a response it will return it back to the caller, but if it is a new request it will create a Handler for the new request doing this multiple times.

## 3 Builder
No to much on this one. An interface that will mark your serviec as a builder and will ensure that each public method that is not called Build is fluid and can be called to set something on the builder.

## 4 Maybe you can suggest something new ...