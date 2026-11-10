# Parts&Stock

**Parts&Stock** is a free, open-source inventory management system written in C#, originally developed to meet the needs of my electronics repair and reselling business, Eclectic Electronics.

The goal is to create a fast, intuitive, and granular inventory management system capable of tracking everything from individual electronic components to complete computer systems.

Parts&Stock is designed with small businesses, repair technicians, resellers, hobbyists, and anyone managing physical inventory in mind.

## Current Status

**Development Status: Pre-release | Active Development**

Parts&Stock is currently in its early CLI (Command-Line Interface) development phase.

This branch is the active development branch. Once version **1.0.0** is ready, a stable release branch will be maintained separately, while development continues here.

The immediate priority is completing a fully functional, free CLI inventory management application before expanding into other interfaces.

### Currently Implemented

The following functionality is implemented or partially implemented:

- **Inventory Creation:** Add inventory items with names, descriptions, prices, quantities, stock counts, and purchase dates.
- **JSON-Based Storage:** Store inventory data locally using JSON files.
- **Item Lookup:** Initial functionality for searching inventory by item name or item number.
- **Storage Location Management:** Initial support for creating storage locations and sublocations.
- **Sourcing Location Management:** Initial support for recording and retrieving sourcing locations.
- **Interactive CLI Menus:** Navigate inventory, sourcing, and storage functions from the terminal.

**Note:** Several features are incomplete or undergoing revision. This development branch may contain bugs, unfinished functionality, and changes to the data format. It is not yet recommended for managing critical inventory without backups.

## Development Roadmap

Development is organized into milestones, with the primary focus on completing the free CLI version.

### Phase 1 CLI Foundation

- [ ] Standardize inventory data models.
- [ ] Centralize JSON file handling and persistence.
- [ ] Improve CLI navigation and input validation.
- [ ] Implement reliable, unique item numbering.

### Phase 2 Core Inventory Management

- [ ] Complete inventory item creation and validation.
- [ ] View complete inventory records.
- [ ] Edit existing inventory items.
- [ ] Delete inventory items with confirmation.
- [ ] Adjust stock and quantity.

### Phase 3 Searching and Filtering

- [ ] Search by item name.
- [ ] Search by item number.
- [ ] Search by storage location.
- [ ] Search by sourcing location.
- [ ] Search by additional inventory fields.
- [ ] Support multiple search results and consistent result formatting.
- [ ] Implement advanced filtering.

### Phase 4 Location Management

- [ ] Complete storage location creation, editing, viewing, and deletion.
- [ ] Support hierarchical storage locations and sublocations.
- [ ] Complete sourcing location management.
- [ ] Associate inventory records with sourcing and storage locations.
- [ ] Prevent accidental deletion of locations referenced by inventory items.

### Phase 5 Stability and Testing

- [ ] Implement automated testing for core inventory operations.
- [ ] Improve error handling and data validation.
- [ ] Protect against accidental data loss.
- [ ] Implement inventory backup and restoration.
- [ ] Test installation and operation on clean systems.
- [ ] Prepare documentation and release packaging.

### Phase 6 CLI v1.0.0 Release

The first stable release will provide the essential functionality needed to manage physical inventory through a terminal interface.

The CLI will remain free and open source.

## Long-Term Roadmap

After completing the CLI version, development will expand toward a locally hosted web application built around the same core inventory management functionality.

Planned future enhancements include:

- **Locally Hosted Web Interface:** Browser-based inventory management accessible across a local network.
- **SQLite Database Backend:** An alternative to JSON based storage.
- **Barcode and QR Code Support:** Generate and scan labels for faster inventory identification.
- **Inventory Statistics:** Analyze stock levels, inventory value, and other useful metrics.
- **Advanced Searching and Filtering:** More powerful inventory queries and reporting.
- **Import and Export:** Support for transferring inventory data between systems and file formats.
- **Expanded Sourcing and Supplier Management:** Additional tools for tracking inventory origins and suppliers.

These features are planned and may change as development progresses.

## Contributing

**Contributions are not just welcome; they're actively encouraged!**

Parts&Stock is an independently developed open-source project, and community involvement is an important part of its development.

Contributions of all experience levels are welcome, including bug fixes, feature implementations, testing, documentation improvements, and code reviews.

If you're interested in contributing:

1. Visit the repository's **Issues** page.
2. Find an issue that interests you and review its scope and acceptance criteria.
3. Discuss questions or proposed changes in the issue before beginning work.
4. Submit a pull request referencing the relevant issue.

### Contribution Guidelines

To keep development organized and maintainable:

- Follow the scope and requirements defined in each issue.
- Avoid unrelated changes or refactoring outside the issue's scope.
- Preserve compatibility with existing inventory data unless a change is explicitly approved.
- Test your changes before submitting a pull request.
- Document significant behavioral changes.
- Use the issue's discussion thread for clarification when necessary.

**Pull requests that extend beyond an issue's defined scope may be declined.** This helps keep contributions focused, reviewable, and compatible with the project's development roadmap.

Feel free to open an issue to suggest improvements, report bugs, or propose new functionality.

## Supporting Development

Parts&Stock is independently developed alongside my electronics repair and reselling business, Eclectic Electronics.

As a physically disabled developer and small business owner, I rely on this work to support myself. If you find Parts&Stock useful and would like to help support its continued development, donations and sponsorships are sincerely appreciated, but never expected.

You can support development through:

- **[GitHub Sponsors](https://github.com/sponsors/stlynnxx)** Support ongoing open-source development through GitHub.
- **[Ko-fi](https://ko-fi.com/H5F5269IH4)** — Make a one time contribution to support the project.

Financial support is entirely optional. Contributions through code, testing, documentation, bug reports, and feedback are equally appreciated.

Thank you for helping Parts&Stock grow!
