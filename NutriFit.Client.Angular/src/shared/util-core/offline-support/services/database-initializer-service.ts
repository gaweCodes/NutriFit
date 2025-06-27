import Dexie, { Table } from 'dexie';
import { Injectable } from '@angular/core';
import { LocalStoreConfig } from '../interfaces/local-store-configuration';
import { AppMetadata } from '../data/app-metadata';

@Injectable({
  providedIn: 'root',
})
export class DatabaseInitializerService {
  private readonly metadataTableName = 'AppMetadata';
  private readonly installationIdentifierKey = 'installationIdentifier';
  private readonly versionKey = 'appVersion';
  private readonly appVersion = '1.0.0';

  public async initializeDatabase(
    db: Dexie,
    transactionTableName: string,
    storeConfig: LocalStoreConfig
  ): Promise<void> {
    db.version(1).stores({
      [this.metadataTableName]: 'key',
      ...storeConfig,
      [transactionTableName]: '++id',
    });

    try {
      await this.ensureInstallationIdentifier(db);
      await this.ensureCorrectVersion(db, [
        transactionTableName,
        ...Object.keys(storeConfig),
      ]);
    } catch (error) {
      console.error('Fehler bei der Initialisierung der Datenbank:', error);
    }
  }

  private async ensureInstallationIdentifier(db: Dexie): Promise<void> {
    const metaTable = db.table<AppMetadata>(this.metadataTableName);
    const existingId = await metaTable.get(this.installationIdentifierKey);

    if (!existingId) {
      await metaTable.put({
        key: this.installationIdentifierKey,
        value: crypto.randomUUID(),
      });
    }
  }

  private async ensureCorrectVersion(
    db: Dexie,
    tablesToClear: string[]
  ): Promise<void> {
    const metaTable = db.table<AppMetadata>(this.metadataTableName);
    const currentVersion = await metaTable.get(this.versionKey);

    if (!currentVersion || currentVersion.value !== this.appVersion) {
      await Promise.all(tablesToClear.map((table) => db.table(table).clear()));
      await metaTable.put({
        key: this.versionKey,
        value: this.appVersion,
      });
    }
  }
}
