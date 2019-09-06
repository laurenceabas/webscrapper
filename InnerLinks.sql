INSERT INTO InnerLinks(WebTargetId, Url, Locator, ElementName, AttributeName, IsCssClass, IsXPath, IsTagName)
VALUES 
	(1, '', '//div//*[@class="results-box standard-search-results"]//*[@class="item-description result-box-item-details"]//*[@class="item-name"]', 'item-name', '', 0, 1, 0),
	(1, '', '//div//*[@class="results-box standard-search-results"]//*[@class="item-description result-box-item-details"]//*[@class="item-start-date"]//*[@class="start-date"]', 'event-date', '', 0, 1, 0),
	(1, '', '//div//*[@class="results-box standard-search-results"]//*[contains(@class, "item-link result-box-item-details last-column limited")]//*[@class="btn btn-primary"]', 'button-element', '', 0, 1, 0),
	(1, '', '//div//*[@id="pricing_list"]//*[@class="legend-ul"]', 'pricing-table', '', 0, 1, 0),
	(1, '', '//*[@class="legend-li"]//*[@class="price-zone-option"]', 'prices', '', 0, 1, 0),
	(1, '', 'zone-label', 'seat-name', '', 1, 0, 0),
	(1, '', 'price-zone-color', 'price-color', 'style', 1, 0, 0),
	(1, '', 'price-zone-price', 'ticket-price', '', 1, 0, 0),
	(1, '', '//div//*[@id="seatGroup"]//*[contains(@style, "fill")]', 'seat-group', '', 0, 1, 0),
	(1, '', '', 'seat-group-style', 'style', 0, 0, 0),
	(1, '', '', 'seat-group-id', 'id', 0, 0, 0),
	(1, '', 'circle', 'seats', 'id', 0, 0, 1),
	(1, '', '', 'seat-desc', 'data-tsdesc', 1, 0, 0),
	(1, '', '', 'seat-status', 'data-status', 1, 0, 0),
	(1, '', '', 'seat-color', 'stroke', 1, 0, 0)