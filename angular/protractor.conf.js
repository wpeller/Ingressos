// Protractor configuration file, see link for more information
// https://github.com/angular/protractor/blob/master/lib/config.ts

const { SpecReporter } = require('jasmine-spec-reporter');
var HTMLReport = require('protractor-html-reporter-2');
var fs = require('fs-extra');
var jasmineReporters = require('jasmine-reporters');

var reportsDirectory = './e2e/reports';
var dashboardReportDirectory = reportsDirectory + '/dashboardReport';
var imagensReports = dashboardReportDirectory + '/imagens';
var detailsReportDirectory = dashboardReportDirectory + '/details'
var HtmlScreenshotReporter = require('protractor-jasmine2-screenshot-reporter');
var ScreenshotAndStackReporter = new HtmlScreenshotReporter({
	dest: detailsReportDirectory,
	filename: 'E2ETestingReport.html',
	reportTitle: "E2E Testing Report",
	showSummary: true,
	reportOnlyFailedSpecs: false,
	captureOnlyFailedSpecs: true,
});


exports.config = {
	restartBrowserBetweenTests: false,
	allScriptsTimeout: 20000,
	specs: ['./e2e/src/features/**/*.e2e-spec.ts'],
	capabilities: {
		browserName: 'chrome',
		chromeOptions: {
			args: ["--headless"]
		}
	},
	directConnect: true,
	baseUrl: 'http://localhost:4200/',
	framework: 'jasmine',
	jasmineNodeOpts: {
		showColors: true,
		defaultTimeoutInterval: 2500000,
		print: function () { },
	},
	beforeLaunch: function () {
		require('ts-node').register({
			project: 'e2e/tsconfig.json',
		});

		return new Promise(function (resolve) {
			ScreenshotAndStackReporter.beforeLaunch(resolve);
		});

	},
	onPrepare() {
		//console logs configurations
		jasmine.getEnv().addReporter(
			new SpecReporter({
				spec: {
					displayStacktrace: true,
					displayErrorMessages: true,
					displayFailed: true,
					displayDuration: true,
				},
				summary: {
					displayErrorMessages: true,
					displayStacktrace: false,
					displaySuccessful: true,
					displayFailed: true,
					displayDuration: true,
				},
				colors: {
					enabled: true,
				},
			})
		);

		// xml report generated for dashboard

		if (!fs.existsSync(reportsDirectory)) {
			fs.mkdirSync(reportsDirectory);
		}

		jasmine.getEnv().addReporter(
			new jasmineReporters.JUnitXmlReporter({
				consolidateAll: true,
				savePath: reportsDirectory + '/xml',
				filePrefix: 'xmlOutput',
			})
		);

		if (!fs.existsSync(dashboardReportDirectory)) {
			fs.mkdirSync(dashboardReportDirectory);
		}

		if (!fs.existsSync(imagensReports)) {
			fs.mkdirSync(imagensReports);
		}

		if (!fs.existsSync(detailsReportDirectory)) {
			fs.mkdirsSync(detailsReportDirectory);
		}

		jasmine.getEnv().addReporter(ScreenshotAndStackReporter);

	},

	onComplete: function () {
		var browserName, browserVersion;
		var capsPromise = browser.getCapabilities();
		capsPromise.then(function (caps) {
			browserName = caps.get('browserName');
			browserVersion = caps.get('version');
			platform = caps.get('platform');

			testConfig = {
				reportTitle: 'Protractor Test Execution Report',
				outputPath: dashboardReportDirectory,
				outputFilename: 'index',
				screenshotPath: imagensReports,
				testBrowser: browserName,
				browserVersion: browserVersion,
				modifiedSuiteName: true,
				screenshotsOnlyOnFailure: false,
				testPlatform: platform,
			};
			new HTMLReport().from(reportsDirectory + '/xml/xmlOutput.xml', testConfig);
		});
	},
};
