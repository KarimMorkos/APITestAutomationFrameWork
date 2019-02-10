Feature: GetCatalogues



Scenario: Get Api Response using Given path
	Given I have this path url /Categories/6327/Details.json
		And using Get Method
	When using this qurey param Catalguge,false
	Then I get api response in json format
