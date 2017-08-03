# Contributing

First off, thank you for considering contributing to the M-Files Developer Portal Samples and Libraries.
By contributing you agree to abide by our [code of conduct](CODE_OF_CONDUCT.md).

## 0. What contributions do you accept?

This repository is designed as a reference point for developers looking to work with the APIs and Frameworks which [M-Files](http://www.m-files.com) exposes.
This repository is designed to be primarily maintained by M-Files staff, but contributions will be considered for merging into the repository.

**We suggest that you [contact devsupport@m-files.com](mailto:devsupport@m-files.com) prior to submitting a pull request, especially if the
pull request will contain a code or process sample.  We will discuss concerns, likelihood of acceptance, and additional content that may be required
from you to support your submission (e.g. a tutorial for the [M-Files Developer Portal](http://developer.m-files.com)) prior to you investing your time.**

At a minimum, all contributions must be:

* Either a **new sample**, an **extension of an existing library**, or a **fix to an existing sample or library**.
* **Consistent in style and format** to other code.
* **Fully commented and clean**. This is a repository for developers to learn.  If your code is not easy to understand then we may ask you to refactor it.
* **Useful to, and usable by,** other developers.  If your sample or code change has limited scope then we may ask you
to maintain your fork separately, and we will point developers across to it if they are interested.

*Not all pull requests will be accepted.  If your pull request is not accepted then please do not take this personally.*

## How do I contribute?

### 1. Research

If you have a question then please initially post on on the
[M-Files Developer Community Yammer network](https://www.yammer.com/m-filesdevelopercommunity).

If you have identified a bug, or an area in which the code could be improved, then
[search the issue tracker](https://github.com/M-Files/MFilesSamplesAndLibraries/issues?q=something)
to see if someone else in the community has already created a ticket.
If not, go ahead and [make one](https://github.com/M-Files/MFilesSamplesAndLibraries/issues/new)!

Please ensure that:

* Bugs should have the phrase `BUG: ` at the start of the title.
A good title would be `BUG: REST API wrapper MFWSVaultObjectOperations.CheckOut does not check out an object`.
* A request for an additional sample should have `SAMPLE: ` at the start of the title.
A good title would be `SAMPLE: UIX application showing a command that's only displayed when certain object types are selected`.
* An issue noting a limitation of an existing library should have `EXTENSION: ` at the start of the title.
A good title for an extension would be `EXTENSION: MFWSVaultObjectPropertyOperations.MarkAssignmentComplete is not implemented`.

### 2. Fork & create a branch

If this is something you think you can fix, then
[fork the repository](https://help.github.com/articles/fork-a-repo)
and create a branch with a descriptive name.

A good branch name would be (where issue #325 is the ticket you're working on):

```sh
git checkout -b 325-rest-wrapper-add-objectpropertyoperations.markassignmentcomplete
```

### 3. Get the test suite running

Download the repository and open it with Visual Studio 2015.  The solution should download all required
packages on build, and should compile with no changes required.

The test suite is written using MSTest and can be [run from within Visual Studio](https://msdn.microsoft.com/en-us/library/ms182470.aspx).
All tests should pass before any changes are made.

### 4. Did you find a bug?

* **Ensure the bug was not already reported** by [searching all
  issues](https://github.com/M-Files/MFilesSamplesAndLibraries/issues?q=).

* If you're unable to find an open issue addressing the problem, [open a new
  one](https://github.com/M-Files/MFilesSamplesAndLibraries/issues/new).  Be sure to
  include a **title and clear description**, as much relevant information as
  possible.  Include:
    * Steps that can be taken to reproduce the issue.
    * M-Files server and versions.
    * Operating systems affected.
    * If possible, a **code sample** or an **executable test case** demonstrating
    the expected behavior that is not occurring.

### 5. Implement your fix or feature

At this point, you're ready to make your changes within your new branch!

### 6. Ensure that your changes or additions work

Confirm that your changes perform as expected.  You should test against the latest
released version of M-Files, and fully document any performance, security or version
requirements.

### 7. Make a pull request

At this point, you should switch back to your master branch and make sure it's
up to date with our's master branch:

```sh
git remote add upstream git@github.com:M-Files/MFilesSamplesAndLibraries.git
git checkout master
git pull upstream master
```

Then update your feature branch from your local copy of master, and push it!

```sh
git checkout 325-rest-wrapper-add-objectpropertyoperations.markassignmentcomplete
git rebase master
git push --set-upstream origin 325-rest-wrapper-add-objectpropertyoperations.markassignmentcomplete
```

Finally, go to GitHub and
[make a pull request](https://help.github.com/articles/creating-a-pull-request)


### 8. Keeping your pull request updated

If a maintainer asks you to "rebase" your pull request, they're saying that a lot of code
has changed since you forked your code, and that you need to update your branch so it's easier to merge.

To learn more about rebasing in Git, there are a lot of
[good](http://git-scm.com/book/en/Git-Branching-Rebasing)
[resources](https://help.github.com/articles/interactive-rebase),
but here's the suggested workflow:

```sh
git checkout 325-rest-wrapper-add-objectpropertyoperations.markassignmentcomplete
git pull --rebase upstream master
git push --force-with-lease 325-rest-wrapper-add-objectpropertyoperations.markassignmentcomplete
```

**Once your code is rebased, you will need to re-run any tests.**

## Guidelines for merging a pull request

A pull request can only be merged by a maintainer if:

* It is passing all tests.
* It has no requested changes.
* It is up to date with current master.
* It passes our other contributing guidelines.